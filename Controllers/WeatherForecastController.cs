using Microsoft.AspNetCore.Mvc;
using Full_Stack_Gruppe_3.Models;
using System.Linq;

namespace Full_Stack_Gruppe_3.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly AppDbContext _context;

        public WeatherForecastController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var forecasts = _context.WeatherForecasts.ToList();
            return View(forecasts);
        }
    }
}
