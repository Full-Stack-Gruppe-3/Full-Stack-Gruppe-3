using Full_Stack_Gruppe_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class ObservationController : Controller
    {
        private readonly ILogger<ObservationController> _logger;
        private readonly ApplicationDbContext _context;

        public ObservationController(ILogger<ObservationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var observations = _context.Observations.ToList();
            _logger.LogInformation("Number of observations retrieved: {Count}", observations.Count);

            var filteredObservations = FilterObservationsByLastSevenDays(observations);
            _logger.LogInformation("Number of filtered observations: {Count}", filteredObservations.Count);

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
