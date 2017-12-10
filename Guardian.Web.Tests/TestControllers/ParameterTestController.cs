﻿using Guardian.Web.Routing.Attributes;

namespace Guardian.Web.Tests.TestControllers
{
    [RoutePrefix("api/testing/parameters")]
    public class ParameterTestController
    {
        [Route("{ID}")]
        public void ID(string ID) { }

        [Route("Method/{ID}")]
        public void MethodID(string ID) { }

        [Route("")]
        public void BodyParameter(string ID) { }
    }
}
