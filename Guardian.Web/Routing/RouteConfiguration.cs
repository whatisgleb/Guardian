using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
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

        public bool IsMatch(string path, string httpRequestMethod)
        {
            if (httpRequestMethod != RequestMethod)
            {
                return false;
            }

            RegexOptions regexOptions = RegexOptions.CultureInvariant
                | RegexOptions.IgnoreCase
                | RegexOptions.Singleline;

            Match match = Regex.Match(
                    path,
                    Path,
                    regexOptions);

            return match.Success;
        }
    }
}
