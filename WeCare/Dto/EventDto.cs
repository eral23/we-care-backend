using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Dto
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventScore { get; set; }
        public string EventResult { get; set; }
        public string EventDetail { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public PatientDto Patient { get; set; }
    }

    public class EventCreateDto
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventScore { get; set; }
        public string EventResult { get; set; }
        public string EventDetail { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public int PatientId { get; set; }
    }
    public class EventUpdateDto
    {
        // TBD: solo actualizacion de EventResult
    }
    public class EventSimpleDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int EventScore { get; set; }
        public string EventResult { get; set; }
        public string EventDetail { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
    }
}
