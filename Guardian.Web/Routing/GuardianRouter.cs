using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Guardian.Web.Abstractions;
using Guardian.Web.Controllers;
using Guardian.Web.Helpers;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Enums;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        internal static RouteHandler GetRouteHandler(IRoutingRequest routingRequest)
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

                // Build pattern to match controller/requestMethod against current path
                RouteAttribute routeAttribute =
                    (RouteAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(RouteAttribute));

                if (routeAttribute.HTTPRequestMethod != routingRequest.HTTPRequestMethod)
                {
                    continue;
                }

                string prefix = GetRoutePrefix(controllerType);
                string pattern = routeAttribute.Route == string.Empty 
                    ? $@"^{prefix}"
                    : $@"^{prefix}/{routeAttribute.Route}";

                Match match = Regex.Match(
                    routingRequest.Path,
                    pattern,
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (match.Success)
                {
                    string queryString = Regex.Replace(
                        routingRequest.Path, 
                        $"{pattern}", "",
                        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    if (routingRequest.HTTPRequestMethod == HTTPRequestMethod.GET 
                        && methodInfo.GetParameters().Any() 
                        && string.IsNullOrWhiteSpace(queryString))
                    {
                        continue;
                    }

                    if (routingRequest.HTTPRequestMethod == HTTPRequestMethod.GET
                        &&!methodInfo.GetParameters().Any() 
                        && !string.IsNullOrWhiteSpace(queryString))
                    {
                        continue;
                    }

                    return new RouteHandler(controllerType, methodInfo, queryString);
                }
            }

            return null;
        }

        private static string GetRoutePrefix(Type type)
        {
            RoutePrefixAttribute routePrefixAttribute =
                (RoutePrefixAttribute) Attribute.GetCustomAttribute(type, typeof(RoutePrefixAttribute));

            string prefix = routePrefixAttribute == null
                ? string.Empty
                : $"/{routePrefixAttribute.RoutePrefix}";

            return prefix;
        }
    }
}
