using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Dto
{
    public class RequisitionDto
    {
        public int RequisitionId { get; set; }
        public string RequisitionStatus { get; set; }
        public PatientDto Patient { get; set; }
        public SpecialistDto Specialist { get; set; }
    }
    public class RequisitionCreateDto
    {
        public int PatientId { get; set; }
        public int SpecialistId { get; set; }
    }
    public class RequisitionUpdateDto
    {
        public int RequisitionId { get; set; }
        public string RequisitionStatus { get; set; }
    }
    public class RequisitionSimpleDto
    {
        public int RequisitionId { get; set; }
        public string RequisitionStatus { get; set; }
        // Posiblemente añadir los ids de los involucrados
    }
}
