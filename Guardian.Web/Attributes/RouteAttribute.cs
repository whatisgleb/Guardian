using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Web.Attributes
{
    public sealed class RouteAttribute : Attribute
    {
        public readonly string Route;

        public RouteAttribute(string route)
        {
            Route = route;
        }
    }
}
