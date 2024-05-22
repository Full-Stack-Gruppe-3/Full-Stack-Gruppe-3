using System;
using System.ComponentModel.DataAnnotations;

namespace Full_Stack_Gruppe_3.Models
{
    public class Observation
    {
        [Key]
        public Guid ElementId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string TimeOffset { get; set; }
        public string TimeResolution { get; set; }
        public int TimeSeriesId { get; set; }
    }
}
