using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers.Api
{
    [RoutePrefix("api/object-graphs")]
    internal class GuardianObjectGraphsApiControllers
    {
        [Route("")]
        public IResponse GetObjectGraphs()
        {
            return new JsonResponse(GuardianOptionsFactory.GetRegisteredObjectGraphNodes());
        }
    }
}
