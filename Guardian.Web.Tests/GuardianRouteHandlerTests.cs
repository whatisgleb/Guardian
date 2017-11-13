using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guardian.Web.Routing;

namespace Guardian.Web.Tests
{
    [TestClass]
    public class GuardianRouteHandlerTests
    {
        [TestMethod]
        public void Given_NoQueryStringParameters_With_ExpectedIntegerParameter_RouteHandler_Works()
        {
            // Arrange
            RouteHandler routeHandler = GetDefaultRouteHandler(string.Empty);

            // Act
            IDictionary<string, object> typedQueryStringParameters = routeHandler.GetTypeCastedQueryStringParameters();

            // Assert
            Assert.IsTrue(typedQueryStringParameters.Count == 0, "Given no route parameters, there should not be any type casted route parameters.");
        }

        [TestMethod]
        public void Given_QueryStringParameters_With_ExpectedIntegerParameter_RouteHandler_Works()
        {
            // Arrange
            RouteHandler routeHandler = GetDefaultRouteHandler("?validationID=1");

            // Act
            IDictionary<string, object> typedQueryStringParameters = routeHandler.GetTypeCastedQueryStringParameters();

            // Assert
            Assert.IsTrue(typedQueryStringParameters.ContainsKey("validationID"), "Expected to correctly identify the 'validationID' parameter.");
            Assert.IsTrue(typedQueryStringParameters["validationID"] is int, "Expected to correctly convert the 'validationID' parameter.");
            Assert.IsTrue(((int)typedQueryStringParameters["validationID"]) == 1, "Expected to correctly convert the 'validationID' parameter.");
        }

        // Write a test for the case where you are making a request with the entire query string being the parameter such as the script loading mechanism

        private RouteHandler GetDefaultRouteHandler(string queryString)
        {
            Type apiControllerType = typeof(GuardianValidationApiController);
            MethodInfo getMethodInfo = apiControllerType.GetMethod(nameof(GuardianValidationApiController.GetValidation));
            return new RouteHandler(apiControllerType, getMethodInfo, queryString);
        }
    }
}
