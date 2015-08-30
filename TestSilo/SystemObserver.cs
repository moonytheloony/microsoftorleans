using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSilo
{
    using GrainInterfaces;

    public class SystemObserver : ISystemObserver
    {
        public void HighTemprature(double value)
        {
            Console.WriteLine("Observed a high temprature {0}", value);
        }
    }
}
