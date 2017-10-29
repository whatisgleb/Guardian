using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Controllers
{
    public class GuardianDashboardController
    {
        [Route("dashboard")]
        public IResponse Dashboard()
        {
            return new Page($"Pages.{nameof(Dashboard)}");
        }

        [Route("index")]
        public IResponse Index()
        {
            return new Page("Content.app.dist.index");
        }
    }
}
