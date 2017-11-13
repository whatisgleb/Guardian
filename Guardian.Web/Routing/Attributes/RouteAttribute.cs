using System;
using Guardian.Web.Routing.Enums;

namespace Guardian.Web.Routing.Attributes
{
    public sealed class RouteAttribute : Attribute
    {
        public readonly string Route;
        public readonly HTTPRequestMethod HTTPRequestMethod;

        public RouteAttribute(string route, HTTPRequestMethod httpRequestMethod = HTTPRequestMethod.GET)
        {
            Route = route;
            HTTPRequestMethod = httpRequestMethod;
        }
    }
}
