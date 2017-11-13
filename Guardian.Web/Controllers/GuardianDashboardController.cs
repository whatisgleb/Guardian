using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers
{
    internal class GuardianDashboardController
    {
        [Route("Dashboard")]
        public IResponse Dashboard()
        {
            return new PageResponse($"Content.app.dist.{nameof(Dashboard).ToLower()}");
        }
    }
}
