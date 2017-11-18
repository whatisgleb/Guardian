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

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal static class GuardianRouter
    {
        private static IEnumerable<RouteConfiguration> _congifuredRoutes;

        public static void BuildRoutes()
        {
            Assembly assembly = ReflectionHelper.GetExecutingAssembly();

            // Methods with RouteAttribute in the current assembly (candidates for routing to)
            IEnumerable<MethodInfo> methodInfos = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsDefined(typeof(RouteAttribute), false));

            _congifuredRoutes = methodInfos
                .Select(GetRouteConfiguration)
                .ToList();
        }

        internal static RouteConfiguration GetRouteConfiguration(MethodInfo methodInfo)
        {
            Type controllerType = methodInfo.ReflectedType;

            RouteAttribute routeAttribute =
                (RouteAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(RouteAttribute));
            RoutePrefixAttribute routePrefixAttribute =
                (RoutePrefixAttribute)Attribute.GetCustomAttribute(controllerType, typeof(RoutePrefixAttribute));

            string prefix = routePrefixAttribute == null
                ? string.Empty
                : $"/{routePrefixAttribute.RoutePrefix}";

            string parameterDelimiter = "{";
            int parameterDelimiterIdx = routeAttribute.Route.IndexOf(parameterDelimiter);
            string route = parameterDelimiterIdx == -1
                ? $"{routeAttribute.Route}$" 
                : routeAttribute.Route.Substring(0, parameterDelimiterIdx);

            string pattern = routeAttribute.Route == string.Empty
                ? $@"^{prefix}$"
                : $@"^{prefix}/{route}";

            return new RouteConfiguration(pattern, routeAttribute.RequestMethod, methodInfo);
        }

        internal static RouteConfiguration GetConfiguredRoute(GuardianRequest request)
        {
            return _congifuredRoutes
                .Where(cr => cr.IsMatch(request.Path, request.Method))
                .FirstOrDefault();
        }

        internal static RouteHandler GetRouteHandler(GuardianRequest request)
        {
            RouteConfiguration matchingRouteConfiguration = _congifuredRoutes
                .Where(rc => rc.IsMatch(request.Path, request.Method))
                .FirstOrDefault();

            if (matchingRouteConfiguration == null)
            {
                return null;
            }

            // Does the targe tmethod have an expected parameter?
            Type parameterType = matchingRouteConfiguration.ControllerMethodInfo.GetParameters()
                .Select(mi => mi.ParameterType)
                .SingleOrDefault();
            List<object> parameters = new List<object>();

            if (parameterType != null)
            {
                if (request.Body != null && request.Body.Length > 0)
                {
                    // Body contains a payload
                    // .. deserialize
                    object parameter = ContentStreamParameterResolver.GetTypedParameter(request.Body, parameterType);

                    parameters.Add(parameter);
                } else 
                {
                    RegexOptions regexOptions = RegexOptions.CultureInvariant
                                                | RegexOptions.IgnoreCase
                                                | RegexOptions.Singleline;

                    string routeParameter = Regex.Replace(
                        request.Path,
                        matchingRouteConfiguration.Path,
                        string.Empty,
                        regexOptions);

                    object parameter;

                    if (string.IsNullOrWhiteSpace(routeParameter))
                    {
                        parameter = parameterType.IsValueType
                            ? Activator.CreateInstance(parameterType)
                            : null;
                    }
                    else
                    {
                        parameter = Convert.ChangeType(routeParameter, parameterType);
                    }

                    parameters.Add(parameter);
                }
            }

            return new RouteHandler(matchingRouteConfiguration.ControllerMethodInfo, parameters);
        }
    }
}
