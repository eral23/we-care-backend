using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Entities;

namespace WeCare.Dto
{
    public class StateDto
    {
        public int StateId { get; set; }
        public int StateBPM { get; set; }
        public int StateSystolicPressure { get; set; }
        public int StateDiastolicPressure { get; set; }
        public string StateDate { get; set; }
        public string StateTime { get; set; }
        public Patient Patient { get; set; }
    }
    public class StateCreateDto
    {
        public int StateBPM { get; set; }
        public int StateSystolicPressure { get; set; }
        public int StateDiastolicPressure { get; set; }
        public int PatientId { get; set; }
    }
    public class StateSimpleCreateDto // Para simular generacion inmediata xd
    {
        public int PatientId { get; set; }
    }
    public class StateSimpleDto
    {
        public int StateId { get; set; }
        public int StateBPM { get; set; }
        public int StateSystolicPressure { get; set; }
        public int StateDiastolicPressure { get; set; }
        public string StateDate { get; set; }
        public string StateTime { get; set; }
    }
}
