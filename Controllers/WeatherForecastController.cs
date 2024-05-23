using Microsoft.AspNetCore.Mvc;
using Full_Stack_Gruppe_3.Models;
using System.Linq;
using System;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly AppDbContext _context;

        public WeatherForecastController(AppDbContext context)
        {
            _context = context;
        }

        // Read: List all forecasts
        public IActionResult Index()
        {
            var forecasts = _context.WeatherForecasts.ToList();
            return View(forecasts);
        }

        // Read: Display details
        public IActionResult Details(Guid id)
        {
            var forecast = _context.WeatherForecasts.Find(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return View(forecast);
        }

        // Create: Display form
        public IActionResult Create()
        {
            return View();
        }

        // Create: Handle form submission
        [HttpPost]
        public IActionResult Create(WeatherForecast forecast)
        {
            if (ModelState.IsValid)
            {
                _context.WeatherForecasts.Add(forecast);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forecast);
        }

        // Update: Display form
        public IActionResult Edit(Guid id)
        {
            var forecast = _context.WeatherForecasts.Find(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return View(forecast);
        }

        // Update: Handle form submission
        [HttpPost]
        public IActionResult Edit(Guid id, WeatherForecast forecast)
        {
            if (id != forecast.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(forecast);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forecast);
        }

        // Delete: Confirm deletion
        public IActionResult Delete(Guid id)
        {
            var forecast = _context.WeatherForecasts.Find(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return View(forecast);
        }

        // Delete: Handle deletion
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var forecast = _context.WeatherForecasts.Find(id);
            if (forecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(forecast);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
