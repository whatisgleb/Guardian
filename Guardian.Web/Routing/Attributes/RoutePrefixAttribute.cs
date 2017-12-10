using System;

namespace Guardian.Web.Routing.Attributes
{
    internal class RoutePrefixAttribute : Attribute
    {
        public readonly string RoutePrefix;

        public RoutePrefixAttribute(string routePrefix)
        {
            RoutePrefix = routePrefix;
        }
    }
}
