using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Guardian.Web.Abstractions;
using Guardian.Web.Controllers;
using Guardian.Web.Helpers;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        private static IEnumerable<RouteConfiguration> _congifuredRoutes;
        private static RegexOptions _regexOptions;

        public static void BuildRoutes(Assembly assembly)
        {
            GuardianRoutingEngine routingEngine = new GuardianRoutingEngine();

            _congifuredRoutes = routingEngine.GetRoutingConfigurations(assembly);
        }

        internal static RouteConfiguration GetConfiguredRoute(GuardianRequest request)
        {
            return _congifuredRoutes
                .Where(cr => cr.IsMatch(request.Path, request.Method))
                .FirstOrDefault();
        }

        internal static RouteHandler GetRouteHandler(GuardianRequest request)
        {
            RouteConfiguration matchingRouteConfiguration = GetConfiguredRoute(request);

            if (matchingRouteConfiguration == null)
            {
                return null;
            }

            // Does the target method have an expected parameter? (Only one is allowed currently)
            Type parameterType = matchingRouteConfiguration.ControllerMethodInfo.GetParameters()
                .Select(mi => mi.ParameterType)
                .SingleOrDefault();

            if (parameterType == null)
            {
                return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo);
            }

            List<object> potentialParameters = new List<object>();

            potentialParameters.Add(GetDeserializedStream(request.Body, parameterType));
            potentialParameters.Add(GetTypedRouteParameter(request.Path, matchingRouteConfiguration.Path, parameterType));

            IEnumerable<object> parameters = potentialParameters
                .Where(p => p != null)
                .ToList();

            return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo, parameters);
        }

        internal static object GetDeserializedStream(Stream contentStream, Type targetType)
        {
            if (contentStream == null || contentStream.Length == 0)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                contentStream.CopyTo(memoryStream);

                byte[] bytes = memoryStream.ToArray();

                string bodyAsString = Encoding.UTF8.GetString(bytes);

                return JsonConvert.DeserializeObject(bodyAsString, targetType);
            }
        }

        internal static object GetTypedRouteParameter(string requestPath, string pathPattern, Type targetType)
        {
            object defaultValue = targetType.IsValueType
                ? Activator.CreateInstance(targetType)
                : null;

            if (string.IsNullOrWhiteSpace(requestPath) || string.IsNullOrWhiteSpace(pathPattern))
            {
                return defaultValue;
            }

            string routeParameter = Regex.Replace(
                requestPath,
                pathPattern,
                string.Empty,
                _regexOptions);

            if (string.IsNullOrWhiteSpace(routeParameter))
            {
                return defaultValue;
            }

            return Convert.ChangeType(routeParameter, targetType);
        }
    }
}
