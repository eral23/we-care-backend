using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientLastname { get; set; }
        public string PatientEmail { get; set; }
        public SpecialistDto Specialist { get; set; }
        public List<EventDto> Events { get; set; }
    }
    
    public class PatientCreateDto
    {
        public string PatientName { get; set; }
        public string PatientLastname { get; set; }
        public string PatientEmail { get; set; }
    }
    public class PatientUpdateDto
    {
        // TBD
    }
    public class PatientSimpleDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientLastname { get; set; }
        public string PatientEmail { get; set; }
    }
}
