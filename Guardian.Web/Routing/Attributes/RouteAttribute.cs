using System;
using Guardian.Web.Routing.Enums;

namespace Guardian.Web.Routing.Attributes
{
    internal sealed class RouteAttribute : Attribute
    {
        public readonly string Route;
        public readonly string RequestMethod;

        public RouteAttribute(string route, string requestMethod = HttpRequestMethod.GET)
        {
            Route = route;
            RequestMethod = requestMethod;
        }
    }
}
