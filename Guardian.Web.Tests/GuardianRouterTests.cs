using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Controllers.Api;
using Guardian.Web.Owin;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Web.Tests
{
    [TestClass]
    public class GuardianRouterTests
    {
        [TestMethod]
        public void GET_Validations_Route()
        {
            RoutingRequest routingRequest = new RoutingRequest("/api/validations", HTTPRequestMethod.GET);

            RouteHandler routeHandler = GuardianRouter.GetRouteHandler(routingRequest);

            Assert.IsTrue(routeHandler != null, "Expected to find a route handler for the given routing request.");
            Assert.IsTrue(routeHandler.ControllerType == typeof(GuardianValidationApiController), "Unexpected route handler controller type.");
            Assert.IsTrue(routeHandler.ControllerMethodInfo.Name == nameof(GuardianValidationApiController.GetValidations), "Unexpected route handler method.");
        }

        [TestMethod]
        public void GET_Validation_Route()
        {
            RoutingRequest routingRequest = new RoutingRequest("/api/validations?validationID=1", HTTPRequestMethod.GET);

            RouteHandler routeHandler = GuardianRouter.GetRouteHandler(routingRequest);

            Assert.IsTrue(routeHandler != null, "Expected to find a route handler for the given routing request.");
            Assert.IsTrue(routeHandler.ControllerType == typeof(GuardianValidationApiController), "Unexpected route handler controller type.");
            Assert.IsTrue(routeHandler.ControllerMethodInfo.Name == nameof(GuardianValidationApiController.GetValidation), "Unexpected route handler method.");
        }

        [TestMethod]
        public void POST_Validations_Route()
        {
            RoutingRequest routingRequest = new RoutingRequest("/api/validations", HTTPRequestMethod.POST);

            RouteHandler routeHandler = GuardianRouter.GetRouteHandler(routingRequest);

            Assert.IsTrue(routeHandler != null, "Expected to find a route handler for the given routing request.");
            Assert.IsTrue(routeHandler.ControllerType == typeof(GuardianValidationApiController), "Unexpected route handler controller type.");
            Assert.IsTrue(routeHandler.ControllerMethodInfo.Name == nameof(GuardianValidationApiController.CreateValidation), "Unexpected route handler controller type.");
            Assert.IsTrue(string.IsNullOrWhiteSpace(routeHandler.QueryString), $"{nameof(GuardianRouter)} unexpectedly identified route parameters.");
        }

        [TestMethod]
        public void Router_Detects_Route_Parameter()
        {
            RoutingRequest routingRequest = new RoutingRequest("/api/validations", HTTPRequestMethod.POST);

            RouteHandler routeHandler = GuardianRouter.GetRouteHandler(routingRequest);

            Assert.IsTrue(routeHandler != null, "Expected to find a route handler for the given routing request.");
            Assert.IsTrue(routeHandler.ControllerType == typeof(GuardianValidationApiController), "Unexpected route handler controller type.");
            Assert.IsTrue(routeHandler.ControllerMethodInfo.Name == nameof(GuardianValidationApiController.CreateValidation), "Unexpected route handler controller type.");
        }
    }
}
