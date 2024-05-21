using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Full_Stack_Gruppe_3.Models
{
    public class RootObject
    {
        
        [Key] public Guid SourceId { get; set; }
        public DateTime ReferenceTime { get; set; }
        public List<Observation> Observations { get; set; }
    }
}
