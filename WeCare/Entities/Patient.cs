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
        public int SpecialistId { get; set; }
        public Specialist Specialist { get; set; }
        public List<Event> Events { get; set; }
    }
}
