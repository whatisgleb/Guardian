using Guardian.Web.Routing.Attributes;

namespace Guardian.Web.Tests.TestControllers
{
    [RoutePrefix("api/testing")]
    internal class RoutePrefixTestController
    {
        [Route("")]
        public void Method() { }
    }

    internal class RoutePrefixLessTestController
    {
        [Route("")]
        public void Method() { }
    }
}
