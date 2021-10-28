using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Entities
{
    public class Specialist
    {
        public int SpecialistId { get; set; }
        public string SpecialistName { get; set; }
        public string SpecialistLastname { get; set; }
        public string SpecialistEmail { get; set; }
        public string SpecialistArea { get; set; }
        public string SpecialistTuitionNumber { get; set; }
        public List<Patient> Patients { get; set; }
    }
}
