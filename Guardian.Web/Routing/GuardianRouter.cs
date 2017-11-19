using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Guardian.Web.Abstractions;
using Guardian.Web.Controllers;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        private static IEnumerable<RouteConfiguration> _congifuredRoutes;
        private static RegexOptions _regexOptions;

        public static void BuildRoutes()
        {
            GuardianRoutingEngine routingEngine = new GuardianRoutingEngine();

            _congifuredRoutes = routingEngine.GetRoutingConfigurations();
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

            // Does the targe tmethod have an expected parameter? (Only one is allowed currently)
            Type parameterType = matchingRouteConfiguration.ControllerMethodInfo.GetParameters()
                .Select(mi => mi.ParameterType)
                .SingleOrDefault();

            if (parameterType == null)
            {
                return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo);
            }

            List<object> potentialParameters = new List<object>();

            potentialParameters.Add(GetDeserializedRequestBody(request, parameterType));
            potentialParameters.Add(GetTypedRouteParameter(request.Path, matchingRouteConfiguration.Path, parameterType));

            IEnumerable<object> parameters = potentialParameters
                .Where(p => p != null)
                .ToList();

            return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo, parameters);
        }

        private static object GetDeserializedRequestBody(GuardianRequest request, Type targetType)
        {
            if (request.Body == null || request.Body.Length == 0)
            {
                return null;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                request.Body.CopyTo(memoryStream);

                byte[] bytes = memoryStream.ToArray();

                string bodyAsString = Encoding.UTF8.GetString(bytes);

                return JsonConvert.DeserializeObject(bodyAsString, targetType);
            }
        }

        private static object GetTypedRouteParameter(string requestPath, string routePath, Type targetType)
        {
            string routeParameter = Regex.Replace(
                requestPath,
                routePath,
                string.Empty,
                _regexOptions);

            if (string.IsNullOrWhiteSpace(routeParameter))
            {
                return targetType.IsValueType
                    ? Activator.CreateInstance(targetType)
                    : null;
            }

            return Convert.ChangeType(routeParameter, targetType);
        }
    }
}
