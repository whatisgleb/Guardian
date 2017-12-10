using Guardian.Web.Routing.Enums;
using System;

namespace Guardian.Web.Routing.Attributes
{
    public sealed class RouteAttribute : Attribute
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
