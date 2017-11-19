using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Routing.Attributes;

namespace Guardian.Web.Tests.TestControllers
{
    [RoutePrefix("api/testing/router")]
    internal class RouterTestController
    {
        [Route("target/{parameter}")]
        public void Target(int parameter) { }
    }
}
