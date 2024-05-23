using System;
using System.ComponentModel.DataAnnotations;

namespace Full_Stack_Gruppe_3.Models
{
    public class WeatherForecast
    {
        [Key] public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public string Comment { get; set; }
    }
}

