using System;
using System.Collections.Generic;

namespace Full_Stack_Gruppe_3.Models
{
    public class RootObject
    {
        public Guid SourceId { get; set; }
        public DateTime ReferenceTime { get; set; }
        public List<Observation> Observations { get; set; }
    }
}
