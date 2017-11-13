using System;
using System.IO;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Helpers;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers
{
    internal class GuardianResourceController
    {
        [Route("resources")]
        public IResponse Resource(string resourceName)
        {
            return new ResourceResponse(resourceName);
        }
    }
}
