using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Web.Attributes
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
