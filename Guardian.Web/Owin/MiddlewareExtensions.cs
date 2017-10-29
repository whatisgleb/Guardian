using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Guardian.Data;
using Guardian.Web.Routing;
using Microsoft.Owin;

namespace Guardian.Web.Owin
{
    using MidFunc = Func<
        Func<IDictionary<string, object>, Task>,
        Func<IDictionary<string, object>, Task>
    >;

    using BuildFunc = Action<
        Func<
            IDictionary<string, object>,
            Func<
                Func<IDictionary<string, object>, Task>,
                Func<IDictionary<string, object>, Task>
            >>>;

    public static class MiddlewareExtensions
    {
        public static BuildFunc UseGuardianDashboard(this BuildFunc builder, GuardianOptions options)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (options == null) throw new ArgumentNullException(nameof(options));

            GuardianOptionsFactory.RegisterOptionsFactory(() => options);

            builder(_ => UseGuardianDashboard());

            return builder;
        }

        public static MidFunc UseGuardianDashboard()
        {
            return
                next =>
                    env =>
                    {
                        GuardianOwinContext context = new GuardianOwinContext(env);
                        RouteHandler handler = GuardianRouter.GetRouteHandler(context);

                        if (handler == null)
                        {
                            return next(env);
                        }

                        return handler.HandleRequest(context);
                    };
        }
    }
}
