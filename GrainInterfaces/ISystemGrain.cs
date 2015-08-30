using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    using Orleans;
    public interface ISystemGrain : IGrainWithIntegerCompoundKey
    {
        Task SetTemprature(TempratureReading reading);

        Task Subscribe(ISystemObserver observer);

        Task Unsubscribe(ISystemObserver observer);

        Task<double> GetTemprature();
    }
}
