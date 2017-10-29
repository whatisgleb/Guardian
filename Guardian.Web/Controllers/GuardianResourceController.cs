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
    public class GuardianResourceController
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

        [Route("css")]
        public IResponse Style()
        {
            return new CssBundle(_stylesheets);
        }

        [Route("scripts")]
        public IResponse Script()
        {
            return new JsBundle(_javascripts);
        }

        [Route("resource")]
        public IResponse Resource(string resourceName)
        {
            return new Resource(resourceName);
        }
    }
}
