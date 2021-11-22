using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Dto
{
    public class SpecialistDto
    {
        public int SpecialistId { get; set; }
        public string SpecialistName { get; set; }
        public string SpecialistLastname { get; set; }
        public string SpecialistEmail { get; set; }
        public string SpecialistArea { get; set; }
        public string SpecialistTuitionNumber { get; set; }
        public List<RequisitionDto> Requisitions { get; set; }
    }
    public class SpecialistCreateDto
    {
        public string SpecialistName { get; set; }
        public string SpecialistLastname { get; set; }
        public string SpecialistEmail { get; set; }
        public string SpecialistArea { get; set; }
        public string SpecialistTuitionNumber { get; set; }
    }
    public class SpecialistUpdateDto
    {
        // TBD: El Update se haria para desvincular un cliente
    }
    public class SpecialistSimpleDto
    {
        public int SpecialistId { get; set; }
        public string SpecialistName { get; set; }
        public string SpecialistLastname { get; set; }
        public string SpecialistEmail { get; set; }
        public string SpecialistArea { get; set; }
        public string SpecialistTuitionNumber { get; set; }
    }
}
