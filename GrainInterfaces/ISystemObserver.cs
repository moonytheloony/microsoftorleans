using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    using Orleans;

    public interface ISystemObserver : IGrainObserver
    {
        void HighTemprature(double value);
    }
}
