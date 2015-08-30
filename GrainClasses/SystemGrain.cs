using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainClasses
{
    using System.Security.Cryptography.X509Certificates;

    using GrainInterfaces;

    using Orleans;

    public class SystemGrain : Grain, ISystemGrain
    {
        private Dictionary<long, double> tempratures;

        private ObserverSubscriptionManager<ISystemObserver> observers;

        public override Task OnActivateAsync()
        {
            this.tempratures = new Dictionary<long, double>();
            // we need to see how many obsevers are there. initialize here.
            this.observers = new ObserverSubscriptionManager<ISystemObserver>();
            // this is just an IDisposable type so that you can cancel the timer.
            var timer = this.RegisterTimer(this.Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

            return base.OnActivateAsync();
        }

        Task Callback(object callbackState)
        {
            var value = this.tempratures.Values.Count == 0 ? 0 : this.tempratures.Values.Average();
            if (value > 100)
            {
                //instead of printing out the values we will notify the observers.
                //Console.WriteLine("Average temprature is high {0}", this.tempratures.Values.Average());
                this.observers.Notify(x => x.HighTemprature(value));
            }

            return TaskDone.Done;
        }

        public Task SetTemprature(TempratureReading reading)
        {
            if (this.tempratures.Keys.Contains(reading.DeviceId))
            {
                this.tempratures[reading.DeviceId] = reading.Value;
            }
            else
            {
                this.tempratures.Add(reading.DeviceId, reading.Value);
            }

            //if (this.tempratures.Values.Average() > 100)
            //{
            //    Console.WriteLine("Average temprature is high {0}", this.tempratures.Values.Average());
            //}

            return TaskDone.Done;
        }

        public Task Subscribe(ISystemObserver observer)
        {
            this.observers.Subscribe(observer);
            return TaskDone.Done;
        }

        public Task Unsubscribe(ISystemObserver observer)
        {
            this.observers.Unsubscribe(observer);
            return TaskDone.Done;
        }


        public Task<double> GetTemprature()
        {
            return Task.FromResult(this.tempratures.Count == 0 ? 0 : this.tempratures.Values.Average());
        }
    }
}
