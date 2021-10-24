using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventScore { get; set; }
        public string EventResult { get; set; }
        public string EventDetail { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
