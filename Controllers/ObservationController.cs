using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Full_Stack_Gruppe_3.Models;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class ObservationController : Controller
    {
        public IActionResult Index()
        {
            // For testing purposes, we'll create a mock list of observations
            var observations = new List<Observation>
            {
                new Observation { ElementId = Guid.NewGuid(), Value = 15.5, Date = DateTime.Now.AddDays(-1), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = 1 },
                new Observation { ElementId = Guid.NewGuid(), Value = 14.3, Date = DateTime.Now.AddDays(-2), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = 2 },
                new Observation { ElementId = Guid.NewGuid(), Value = 16.7, Date = DateTime.Now.AddDays(-3), TimeOffset = "UTC+1", TimeResolution = "hourly", TimeSeriesId = 3 }
            };

            return View(observations);
        }
    }
}
