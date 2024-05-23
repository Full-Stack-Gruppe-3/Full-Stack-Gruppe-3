using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Full_Stack_Gruppe_3.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class WeatherDataImporter : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherDataImporter> _logger;
    private readonly string clientId = "10e02932-6bce-463a-b1e0-e997285d2071";
    private readonly string clientSecret = "67be8d57-2d8f-4e9e-80e8-ed1447d54d20";

    public WeatherDataImporter(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory, ILogger<WeatherDataImporter> logger)
    {
        _serviceProvider = serviceProvider;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ImportWeatherDataAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during weather data import.");
            }
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Run daily
        }
    }

    private async Task ImportWeatherDataAsync()
    {
        _logger.LogInformation("Starting data import at {Time}", DateTime.Now);
        var client = _httpClientFactory.CreateClient();

        // Legg til klientidentifikasjon hvis nødvendig
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

        // Angi URL med ønsket tidsintervall (de siste 7 dagene)
        DateTime fromDate = DateTime.Today.AddDays(-7);
        DateTime toDate = DateTime.Today;

        var apiUrl = $"https://frost.met.no/observations/v0.jsonld?sources=SN18700&referencetime={fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}&elements=air_temperature";
        _logger.LogInformation("Sending request to API: {Url}", apiUrl);

        var response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("API response: {Json}", json);

            // Hent ut observasjoner fra JSON-responsen
            var observations = await ParseObservationsFromJsonAsync(json);
            if (observations == null || observations.Count == 0)
            {
                _logger.LogWarning("No observations found in the API response.");
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                _logger.LogInformation("Saving {Count} observations to the database.", observations.Count);

                foreach (var observation in observations)
                {
                    // Lagre observasjon i databasen
                    dbContext.Observations.Add(new Observation
                    {
                        ElementId = Guid.NewGuid(), // Generer et unikt ID
                        Value = observation.Temperature,
                        Date = observation.Date
                    });
                }

                await dbContext.SaveChangesAsync();
                _logger.LogInformation("Number of observations in database after save: {Count}", dbContext.Observations.Count());
            }
        }
        else
        {
            _logger.LogError("Failed to retrieve data from API. Status code: {StatusCode}, Reason: {Reason}", response.StatusCode, response.ReasonPhrase);
        }
        _logger.LogInformation("Completed data import at {Time}", DateTime.Now);
    }

    private async Task<List<(DateTime Date, double Temperature)>> ParseObservationsFromJsonAsync(string json)
    {
        // Deserialize JSON-responsen til et JObject
        JObject responseObject = JObject.Parse(json);

        // Hent ut data-delen av responsen
        JArray data = (JArray)responseObject["data"];

        // Opprett en liste for å lagre observasjoner
        List<(DateTime Date, double Temperature)> observations = new List<(DateTime Date, double Temperature)>();

        // Gå gjennom hver observasjon i data-delen
        foreach (var observationData in data)
        {
            // Hent ut dato og temperatur for hver observasjon
            DateTime date = (DateTime)observationData["referenceTime"];
            double temperature = (double)observationData["observations"][0]["value"];

            // Legg til dato og temperatur i listen
            observations.Add((date, temperature));
        }

        return observations;
    }
}
