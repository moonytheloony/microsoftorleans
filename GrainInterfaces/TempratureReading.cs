using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    using Orleans.Concurrency;

    [Immutable]
    public class TempratureReading
    {
        public double Value { get; set; }

        public long DeviceId { get; set; }

        public DateTime Time { get; set; }
    }
}
