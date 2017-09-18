using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guardian.Web.Owin;
using Owin;

namespace Guardian.Web.Extensions
{
    using BuildFunc = Action<
        Func<
            IDictionary<string, object>,
            Func<
                Func<IDictionary<string, object>, Task>,
                Func<IDictionary<string, object>, Task>
        >>>;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseGuardianDashboard(this IAppBuilder builder)
        {
            builder.Map("/guardian",
                subApp => subApp
                    .UseOwin()
                    .UseGuardianDashboard());

            return builder;
        }
        private static BuildFunc UseOwin(this IAppBuilder builder)
        {
            return middleware => builder.Use(middleware(builder.Properties));
        }
    }
}
