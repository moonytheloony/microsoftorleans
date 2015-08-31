using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication
{
    using Microsoft.WindowsAzure.ServiceRuntime;

    using Orleans.Runtime.Host;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            if (RoleEnvironment.IsAvailable || RoleEnvironment.IsEmulated)
            {
                // azure
                AzureClient.Initialize(this.Server.MapPath("AzureClientConfiguration.xml"));
            }
            else
            {
                //premise
                Orleans.GrainClient.Initialize(this.Server.MapPath("DevTestClientConfiguration.xml"));
            }
        }
    }
}
