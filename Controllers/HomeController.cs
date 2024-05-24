using Full_Stack_Gruppe_3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // For testing purposes, we'll create a mock list of observations
            var observations = new List<Observation>
            {
                new Observation { ElementId = Guid.NewGuid(), Value = 15.5, Date = DateTime.Now.AddDays(-1), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid() },
                new Observation { ElementId = Guid.NewGuid(), Value = 14.3, Date = DateTime.Now.AddDays(-2), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid()},
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-6), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid() },
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-10), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid() },
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-12), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid() },
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-8), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId =  Guid.NewGuid() },
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-9), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = Guid.NewGuid() },
            };

            var filteredObservations = FilterObservationsByLastSevenDays(observations);
            foreach (var obs in filteredObservations)
            {
                Console.WriteLine($"ElementId: {obs.ElementId}, Value: {obs.Value}, Date: {obs.Date}, TimeSeriesId: {obs.TimeSeriesId}");
            }


            return View(filteredObservations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private static List<Observation> FilterObservationsByLastSevenDays(List<Observation> observations)
        {
            return observations.Where(obs => (DateTime.Now - obs.Date).TotalDays <= 7).ToList();
        }
    }
}

