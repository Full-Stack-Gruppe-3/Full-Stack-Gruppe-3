using Microsoft.EntityFrameworkCore;
using Full_Stack_Gruppe_3.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Legg til tjenester til containeren
builder.Services.AddControllersWithViews();


// Configure DbContext with SQL Server and enable retry on failure
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    }));

// Add HttpClient for making HTTP requests
builder.Services.AddHttpClient();

// Register the background service
builder.Services.AddHostedService<WeatherDataImporter>();

// Legg til AppDbContext-tjenesten
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=weather.db")); // Eller bruk UseSqlServer for MS SQL LocalDB


var app = builder.Build();

// Sørg for at databasen er opprettet og migrert
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Konfigurer HTTP-forespørselspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    app.Run();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An unhandled exception occurred during bootstrapping.");
    throw;
}
