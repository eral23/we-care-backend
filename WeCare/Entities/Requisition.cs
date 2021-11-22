using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Entities
{
    public class Requisition
    {
        public int RequisitionId { get; set; }
        public string RequisitionStatus { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int SpecialistId { get; set; }
        public Specialist Specialist { get; set; }
    }
}
