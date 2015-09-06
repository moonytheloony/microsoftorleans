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

    public class SystemController : ApiController
    {
        public Task<double> Get(string id)
        {
            var systemGrain = GrainClient.GrainFactory.GetGrain<ISystemGrain>(0, keyExtension: id);
            return systemGrain.GetTemprature();
        }

        public void Post([FromBody] string deviceSystemCsv)
        {
            var csv = deviceSystemCsv.Split(',');
            var deviceGrain = GrainClient.GrainFactory.GetGrain<IDeviceGrain>(Convert.ToInt32(csv[0]));
            deviceGrain.JoinSystem(csv[1]).Wait();
        }
    }
}
