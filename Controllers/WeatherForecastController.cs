using Microsoft.AspNetCore.Mvc;
using Full_Stack_Gruppe_3.Models;
using System;
using System.Collections.Generic;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class WeatherForecastController : Controller
    {
        public IActionResult Index()
        {
            // Hardcode some weather forecast data
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Id = Guid.NewGuid(), Date = DateTime.Now, Temperature = 23.4, Comment = "Sunny" },
                new WeatherForecast { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(1), Temperature = 19.7, Comment = "Partly cloudy" },
                new WeatherForecast { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(2), Temperature = 21.2, Comment = "Rainy" }
            };

            return View(forecasts);
        }
    }
}
