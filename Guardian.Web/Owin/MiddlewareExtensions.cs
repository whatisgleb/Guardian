using Guardian.Web.Helpers;
using Guardian.Web.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guardian.Web.Owin
{
    using BuildFunc = Action<
        Func<
            IDictionary<string, object>,
            Func<
                Func<IDictionary<string, object>, Task>,
                Func<IDictionary<string, object>, Task>
            >>>;
    using MidFunc = Func<
        Func<IDictionary<string, object>, Task>,
        Func<IDictionary<string, object>, Task>
    >;

    public static class MiddlewareExtensions
    {
        public static BuildFunc UseGuardianDashboard(this BuildFunc builder, GuardianOptions options)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (options == null) throw new ArgumentNullException(nameof(options));

            GuardianOptionsFactory.RegisterOptionsFactory(() => options);
            GuardianRouter.BuildRoutes(ReflectionHelper.GetExecutingAssembly());

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
                        RouteHandler handler = GuardianRouter.GetRouteHandler(context.Request);

                        if (handler == null)
                        {
                            return next(env);
                        }

                        return handler.HandleRequest(context);
                    };
        }
    }
}
