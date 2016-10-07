using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WorkingHours.Web.App_Start.Startup))]

namespace WorkingHours.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthConfig.Register(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
