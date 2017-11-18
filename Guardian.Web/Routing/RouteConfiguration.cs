using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Guardian.Web.Routing.Enums;

[assembly: InternalsVisibleTo("Guardian.Web.Tests")]
namespace Guardian.Web.Routing
{
    internal class RouteConfiguration
    {
        public string Path { get; set; }
        public string HttpRequestMethod { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public RouteConfiguration(string path, string httpRequestMethod, MethodInfo methodInfo)
        {
            Path = path;
            HttpRequestMethod = httpRequestMethod;
            MethodInfo = methodInfo;
        }

        public bool IsMatch(string path, string httpRequestMethod)
        {
            if (httpRequestMethod != HttpRequestMethod)
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
