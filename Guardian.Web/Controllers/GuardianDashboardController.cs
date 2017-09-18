using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Attributes;

namespace Guardian.Web.Controllers
{
    public class GuardianDashboardController : GuardianBaseController
    {
        public GuardianDashboardController(GuardianContext context) : base(context)
        {
        }

        [Route("dashboard")]
        public Task Dashboard()
        {
            return Page(nameof(Dashboard));
        }
    }
}
