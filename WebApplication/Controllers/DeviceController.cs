using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    using System.Threading.Tasks;

    using GrainInterfaces;

    using Orleans;

    public class DeviceController : ApiController
    {
        public Task<double> Get(double id)
        {
            var deviceGrain = GrainClient.GrainFactory.GetGrain<IDeviceGrain>(4);
            return deviceGrain.GetTemprature();
        }

        public Task Post([FromBody] string message)
        {
            var decodeGrain = GrainClient.GrainFactory.GetGrain<IDecodeGrain>(0);
            return decodeGrain.Decode(message);
        }
    }
}
