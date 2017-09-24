using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Guardian.Web.Abstractions;
using Guardian.Web.Controllers;
using Guardian.Web.Helpers;
using Guardian.Web.Routing.Attributes;

namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        public static RouteHandler GetRouteHandler(GuardianContext context)
        {
            Assembly assembly = ReflectionHelper.GetExecutingAssembly();

            // Methods with RouteAttribute in the current assembly (candidates for routing to)
            IEnumerable<MethodInfo> methodInfos = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsDefined(typeof(RouteAttribute), false));

            foreach (MethodInfo methodInfo in methodInfos)
            {
                // If the controller has a RoutePrefix attribute, we need to build the pattern matching prefix
                Type controllerType = methodInfo.ReflectedType;

                RoutePrefixAttribute routePrefixAttribute =
                    (RoutePrefixAttribute)Attribute.GetCustomAttribute(controllerType, typeof(RoutePrefixAttribute));

                string prefix = routePrefixAttribute == null
                    ? string.Empty
                    : $"/{routePrefixAttribute.RoutePrefix}";

                // Build pattern to match controller/method against current path
                RouteAttribute routeAttribute =
                    (RouteAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(RouteAttribute));

                if (!string.Equals(routeAttribute.Verb.ToString(), context.Request.Method,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                string pattern = routeAttribute.Route == string.Empty 
                    ? $@"^{prefix}"
                    : $@"^{prefix}/{routeAttribute.Route}";

                Match match = Regex.Match(
                    context.Request.Path,
                    pattern,
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (match.Success)
                {
                    return new RouteHandler(controllerType, methodInfo);
                }
            }

            return null;
        }
    }
}
