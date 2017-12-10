using Guardian.Web.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        private static IEnumerable<RouteConfiguration> _congifuredRoutes;
        private static RegexOptions _regexOptions;

        /// <summary>
        /// Builds and caches route configurations 
        /// </summary>
        /// <param name="assembly"></param>
        public static void BuildRoutes(Assembly assembly)
        {
            GuardianRoutingEngine routingEngine = new GuardianRoutingEngine();

            _congifuredRoutes = routingEngine.GetRoutingConfigurations(assembly);
        }

        /// <summary>
        /// Returns a route configuration if the request matches a configured route.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static RouteConfiguration GetConfiguredRoute(GuardianRequest request)
        {
            return _congifuredRoutes
                .Where(cr => cr.IsMatch(request.Path, request.Method))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a Route Handler for the given request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

            return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo, GetRequestParameters(request, parameterType, matchingRouteConfiguration));
        }

        /// <summary>
        /// Deserializes the specified stream into an object of the specified type.
        /// </summary>
        /// <param name="contentStream"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Deserializes the specified request path into a parameter of the specified type.
        /// </summary>
        /// <param name="requestPath"></param>
        /// <param name="pathPattern"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns non-null parameters foudn in given request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameterType"></param>
        /// <param name="routeConfiguration"></param>
        /// <returns></returns>
        private static object[] GetRequestParameters(GuardianRequest request, Type parameterType, RouteConfiguration routeConfiguration)
        {
            List<object> potentialParameters = new List<object>();

            potentialParameters.Add(GetDeserializedStream(request.Body, parameterType));
            potentialParameters.Add(GetTypedRouteParameter(request.Path, routeConfiguration.Path, parameterType));

            return potentialParameters
                .Where(p => p != null)
                .ToArray();
        }
    }
}
