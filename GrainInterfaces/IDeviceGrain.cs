using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
    public interface IDeviceGrain : IGrainWithIntegerKey
    {
        Task SetTemprature(double temprature);

        Task<double> GetTemprature();

        // join system
        Task JoinSystem(string name);
    }
}
