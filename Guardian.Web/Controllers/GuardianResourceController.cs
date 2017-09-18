using System;
using System.IO;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Attributes;
using Guardian.Web.Helpers;

namespace Guardian.Web.Controllers
{
    [RoutePrefix("content")]
    public class GuardianResourceController : GuardianBaseController
    {
        private string[] _javascripts = new string[]
        {
            "jquery-3.2.1.min.js",
            "bootstrap.min.js"
        };

        private string[] _stylesheets = new string[]
        {
            "bootstrap.min.css"
        };

        public GuardianResourceController(GuardianContext context) : base(context)
        {
        }

        [Route("css")]
        public Task Style()
        {
            return Resource(_stylesheets, "Content.css", "text/css");
        }

        [Route("scripts")]
        public Task Script()
        {
            return Resource(_javascripts, "Content.scripts", "text/javascript");
        }
    }
}
