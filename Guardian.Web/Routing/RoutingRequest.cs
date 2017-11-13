using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Routing.Enums;

namespace Guardian.Web.Routing
{
    internal class RoutingRequest : IRoutingRequest
    {
        public RoutingRequest(string path, string httpRequestMethod)
        {
            Path = path;
            HTTPRequestMethod = (HTTPRequestMethod) Enum.Parse(typeof(HTTPRequestMethod), httpRequestMethod, true);
        }

        public RoutingRequest(string path, HTTPRequestMethod httpRequestMethod)
        {
            Path = path;
            HTTPRequestMethod = httpRequestMethod;
        }

        public string Path { get; set; }
        public HTTPRequestMethod HTTPRequestMethod { get; set; }
    }
}
