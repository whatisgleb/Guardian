using System;
using System.Threading.Tasks;
using Guardian.Web;
using Guardian.Web.Extensions;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Guardian.Website.Startup))]

namespace Guardian.Website
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseGuardianDashboard();
        }
    }
}
