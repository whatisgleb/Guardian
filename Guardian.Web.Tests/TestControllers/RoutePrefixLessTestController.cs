using Guardian.Web.Routing.Attributes;

namespace Guardian.Web.Tests.TestControllers
{
    internal class RoutePrefixLessTestController
    {
        [Route("")]
        public void Method() { }
    }
}