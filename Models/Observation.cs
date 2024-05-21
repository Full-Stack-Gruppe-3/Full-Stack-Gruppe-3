using System;

namespace Full_Stack_Gruppe_3.Models
{
    public class Observation
    {
        public Guid Elementid { get; set; }
        public double Value { get; set; } 
        public DateTime Date { get; set; }
        public string TimeOffset { get; set; }
        public string TimeResolution { get; set; }
        public int TimeSeriesid { get; set; }

    }
}
