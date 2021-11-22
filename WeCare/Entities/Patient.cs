using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientLastname { get; set; }
        public string PatientEmail { get; set; }
        public bool PatientLinked {get; set;}
        public List<Requisition> Requisitions { get; set; }
        public List<Event> Events { get; set; }
        public List<State> States { get; set; }
    }
}
