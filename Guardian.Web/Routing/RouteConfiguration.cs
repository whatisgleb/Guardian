using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    /// <summary>
    /// This class represents a known route configuration. A route configuration can be used to compare against an incoming request in order to identify a matching known/configured route.
    /// </summary>
    internal class RouteConfiguration
    {
        public string Path { get; set; }
        public string RequestMethod { get; set; }
        public MethodInfo ControllerMethodInfo { get; set; }

        public RouteConfiguration(string path, string requestMethod, MethodInfo controllerMethodInfo)
        {
            Path = path;
            RequestMethod = requestMethod;
            ControllerMethodInfo = controllerMethodInfo;
        }

        /// <summary>
        /// Checks if the given request matches this route configuration.
        /// </summary>
        /// <param name="requestPath"></param>
        /// <param name="httpRequestMethod"></param>
        /// <returns></returns>
        public bool IsMatch(string requestPath, string httpRequestMethod)
        {
            if (httpRequestMethod != RequestMethod)
            {
                return false;
            }

            RegexOptions regexOptions = RegexOptions.CultureInvariant
                | RegexOptions.IgnoreCase
                | RegexOptions.Singleline;

            Match match = Regex.Match(
                    requestPath,
                    Path,
                    regexOptions);

            return match.Success;
        }
    }
}
