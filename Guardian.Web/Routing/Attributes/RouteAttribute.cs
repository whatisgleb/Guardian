using System;

namespace Guardian.Web.Routing.Attributes
{
    public sealed class RouteAttribute : Attribute
    {
        public readonly string Route;
        public readonly HTTP Verb;

        public RouteAttribute(string route, HTTP verb = HTTP.GET)
        {
            Route = route;
            Verb = verb;
        }
    }
}
