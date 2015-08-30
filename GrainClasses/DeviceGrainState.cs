using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainClasses
{
    using Orleans;

    public class DeviceGrainState : GrainState
    {
        public double LastValue { get; set; }
        public string System { get; set; }
    }
}
