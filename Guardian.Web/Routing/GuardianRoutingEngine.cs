using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Guardian.Web.Helpers;
using Guardian.Web.Routing.Attributes;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal class GuardianRoutingEngine
    {
        private const string _routeParameterDelimiter = "{";

        public IEnumerable<RouteConfiguration> GetRoutingConfigurations(Assembly assembly)
        {
            // Methods with RouteAttribute in the current assembly (candidates for routing to)
            IEnumerable<MethodInfo> methodInfos = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsDefined(typeof(RouteAttribute), false));

            return methodInfos
                .Select(GetRouteConfiguration)
                .ToList();
        }

        internal RouteConfiguration GetRouteConfiguration(MethodInfo controllerMethodInfo)
        {
            RouteAttribute routeAttribute =
                (RouteAttribute)Attribute.GetCustomAttribute(controllerMethodInfo, typeof(RouteAttribute));

            string prefix = GetRoutePrefix(controllerMethodInfo);
            string suffix = GetRouteSuffix(controllerMethodInfo);

            return new RouteConfiguration($@"^{prefix}{suffix}", routeAttribute.RequestMethod, controllerMethodInfo);
        }

        internal string GetRoutePrefix(MethodInfo controllerMethodInfo)
        {
            string prefix = string.Empty;

            Type controllerType = controllerMethodInfo.ReflectedType;

            RoutePrefixAttribute routePrefixAttribute =
                (RoutePrefixAttribute)Attribute.GetCustomAttribute(controllerType, typeof(RoutePrefixAttribute));

            if (routePrefixAttribute != null)
            {
                prefix = $"/{routePrefixAttribute.RoutePrefix}";
            }

            return prefix;
        }

        internal string GetRouteSuffix(MethodInfo controllerMethodInfo)
        {
            string suffix = "$";

            RouteAttribute routeAttribute =
                (RouteAttribute)Attribute.GetCustomAttribute(controllerMethodInfo, typeof(RouteAttribute));

            if (string.IsNullOrWhiteSpace(routeAttribute.Route))
            {
                return suffix;
            }

            int routeParameterIdx = routeAttribute.Route.IndexOf(_routeParameterDelimiter);

            return routeParameterIdx == -1
                ? $"/{routeAttribute.Route}$"
                : $"/{routeAttribute.Route.Substring(0, routeParameterIdx)}";
        }
    }
}