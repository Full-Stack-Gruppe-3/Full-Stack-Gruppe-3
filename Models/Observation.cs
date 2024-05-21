using System;
using System.ComponentModel.DataAnnotations;

namespace Full_Stack_Gruppe_3.Models
{
    public class Observation
    {
        [Key] public Guid Elementid { get; set; }
        public double Value { get; set; } 
        public DateTime Date { get; set; }
        public string TimeOffset { get; set; }
        public string TimeResolution { get; set; }
        public Guid TimeSeriesId { get; set; }


        public RootObject RootObject { get; set; }
        public Level Level { get; set; }
    }
}
