using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Routing.Enums;

namespace Guardian.Web.Routing
{
    internal interface IRoutingRequest
    {
        string Path { get; set; }
        HTTPRequestMethod HTTPRequestMethod { get; set; }
    }
}
