﻿using System;
using System.Threading.Tasks;
using Guardian.Web;
using Guardian.Web.Extensions;
using Guardian.Website.EntityFramework;
using Guardian.Website.Guardian;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Guardian.Website.Startup))]

namespace Guardian.Website
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string applicationID = "DemoApplication";

            app.UseGuardianDashboard(new GuardianOptions(applicationID)
            {
                GuardianDataProviderFactory = () => new ApplicationValidationDataProvider(() => new ApplicationDbContext())
            });
        }
    }
}