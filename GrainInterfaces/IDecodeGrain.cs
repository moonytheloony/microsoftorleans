using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    using Orleans;
    using Orleans.Concurrency;

    public interface IDecodeGrain : IGrainWithIntegerKey
    {
        Task Decode(string message);
    }
}
