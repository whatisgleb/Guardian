using System;

namespace Guardian.Web.Routing.Attributes
{
    public class RoutePrefixAttribute : Attribute
    {
        public readonly string RoutePrefix;

        public RoutePrefixAttribute(string routePrefix)
        {
            RoutePrefix = routePrefix;
        }
    }
}
