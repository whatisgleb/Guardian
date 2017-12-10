using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers
{
    internal class GuardianResourceController
    {
        [Route("resources/{resourceName}")]
        public IResponse Resource(string resourceName)
        {
            return new ResourceResponse(resourceName);
        }
    }
}
