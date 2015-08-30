using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainClasses
{
    using GrainInterfaces;

    using Orleans;
    using Orleans.Concurrency;

    [StatelessWorker]
    [Reentrant]
    public class DecodeGrain : Grain, IDecodeGrain
    {
        public Task Decode(string message)
        {
            var parts = message.Split(',');
            var grain = this.GrainFactory.GetGrain<IDeviceGrain>(long.Parse(parts[0]));
            return grain.SetTemprature(double.Parse(parts[1]));
        }
    }
}
