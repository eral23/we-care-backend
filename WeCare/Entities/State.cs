using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Entities
{
    public class State
    {
        public int StateId { get; set; }
        public int StateBPM { get; set; }
        public int StateSystolicPressure { get; set; }
        public int StateDiastolicPressure { get; set; }
        public string StateDate { get; set; }
        public string StateTime { get; set;  }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
