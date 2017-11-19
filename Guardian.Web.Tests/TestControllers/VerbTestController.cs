using Guardian.Web.Routing.Attributes;
using Guardian.Web.Routing.Enums;

namespace Guardian.Web.Tests.TestControllers
{
    [RoutePrefix("api/testing/verbs")]
    internal class VerbTestController
    {
        [Route("")]
        public void GET() { }

        [Route("", HttpRequestMethod.PUT)]
        public void PUT() { }

        [Route("", HttpRequestMethod.POST)]
        public void POST() { }

        [Route("", HttpRequestMethod.DELETE)]
        public void DELETE() { }
    }
}
