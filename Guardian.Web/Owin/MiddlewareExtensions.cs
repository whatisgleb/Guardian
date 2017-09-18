﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
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
        public static BuildFunc UseGuardianDashboard(this BuildFunc builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder(_ => UseGuardianDashboard());

            return builder;
        }

        public static MidFunc UseGuardianDashboard()
        {
            return
                next =>
                    env =>
                    {
                        var context = new GuardianOwinContext(env);
                        
                        RouteHandler handler = GuardianRouter.GetRouteHandler(context.Request.Path);

                        if (handler == null)
                        {
                            return next(env);
                        }

                        return handler.Execute(context);
                    };
        }

        private static Task Unauthorized(IOwinContext owinContext)
        {
            var isAuthenticated = owinContext.Authentication?.User?.Identity?.IsAuthenticated;

            owinContext.Response.StatusCode = isAuthenticated == true
                ? (int) HttpStatusCode.Forbidden
                : (int) HttpStatusCode.Unauthorized;

            return Task.FromResult(0);
        }
    }
}
