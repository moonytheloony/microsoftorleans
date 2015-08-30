using System.Threading.Tasks;
using Orleans;

namespace GrainClasses
{
    using System;

    using GrainInterfaces;

    using Orleans.Concurrency;
    using Orleans.Providers;

    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    [StorageProvider(ProviderName = "AzureStore")]
    [Reentrant]
    public class DeviceGrain : Grain<DeviceGrainState>, IDeviceGrain
    {
        public override Task OnActivateAsync()
        {
            Console.WriteLine("Activated grain {0}", this.GetPrimaryKeyLong());
            Console.WriteLine("Activated state {0}", this.State.LastValue);
            return base.OnActivateAsync();
        }

        public async Task SetTemprature(double temprature)
        {
            if (this.State.LastValue < 100 && temprature > 100)
            {
                Console.WriteLine("High temprature {0}", temprature);
            }

            //// change state when needed.
            if (this.State.LastValue != temprature)
            {
                this.State.LastValue = temprature;
                await this.WriteStateAsync();
            }

            var systemGrain = this.GrainFactory.GetGrain<ISystemGrain>(0, keyExtension: this.State.System);
            var reading = new TempratureReading { DeviceId = this.GetPrimaryKeyLong(), Time = DateTime.Now, Value = temprature };
            await systemGrain.SetTemprature(reading);
        }

        public Task JoinSystem(string name)
        {
            this.State.System = name;
            return TaskDone.Done;
        }


        public Task<double> GetTemprature()
        {
            return Task.FromResult(this.State.LastValue);
        }
    }
}
