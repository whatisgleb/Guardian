using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Enums;
using Guardian.Web.Tests.TestControllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Web.Tests
{
    [TestClass]
    public class GuardianRoutingEngineTests
    {
        private readonly GuardianRoutingEngine _routingEngine;
        private readonly IEnumerable<RouteConfiguration> _routeConfigurations;

        public GuardianRoutingEngineTests()
        {
            _routingEngine = new GuardianRoutingEngine();
            _routeConfigurations = _routingEngine.GetRoutingConfigurations(typeof(GuardianRoutingEngineTests).Assembly);
        }

        [TestMethod]
        public void Given_Controller_Without_RoutePrefix_Expect_EmptyStringPrefix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(RoutePrefixLessTestController).GetMethod("Method");

            // Act
            string prefix = _routingEngine.GetRoutePrefix(methodInfo);

            // Assert
            prefix.Should().BeEmpty();
        }

        [TestMethod]
        public void Given_Controller_With_RoutePrefix_Expect_MatchingPrefix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(RoutePrefixTestController).GetMethod("Method");

            // Act
            string prefix = _routingEngine.GetRoutePrefix(methodInfo);

            // Assert
            prefix.Should().Be("/api/testing");
        }

        [TestMethod]
        public void Given_Controller_With_AllVerbMethods_Expect_EngineToDetectAllOfThem()
        {
            // Arrange
            ICollection<string> expectedRequestMethods = new List<string>()
            {
                HttpRequestMethod.GET,
                HttpRequestMethod.PUT,
                HttpRequestMethod.POST,
                HttpRequestMethod.DELETE
            };

            // Act

            // Assert
            ICollection<string> actualRequestMethods = _routeConfigurations
                .Select(rc => rc.RequestMethod)
                .Distinct()
                .ToList();

            actualRequestMethods.Should().BeEquivalentTo(expectedRequestMethods);
        }

        [TestMethod]
        public void Given_Method_With_EmptyRoute_Expect_LineEndSuffix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(VerbTestController).GetMethod("GET");

            // Act
            string suffix = _routingEngine.GetRouteSuffix(methodInfo);

            // Assert
            suffix.Should().Be("$");
        }

        [TestMethod]
        public void Parameter_Routing_Simple_Suffix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ParameterTestController).GetMethod("ID");

            // Act
            string suffix = _routingEngine.GetRouteSuffix(methodInfo);

            // Assert
            suffix.Should().Be("/");
        }

        [TestMethod]
        public void Parameter_Routing_Complex_Suffix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ParameterTestController).GetMethod("MethodID");

            // Act
            string suffix = _routingEngine.GetRouteSuffix(methodInfo);

            // Assert
            suffix.Should().Be("/Method/");
        }

        [TestMethod]
        public void Parameter_Routing_Body_Parameter_Suffix()
        {
            // Arrange
            MethodInfo methodInfo = typeof(ParameterTestController).GetMethod("BodyParameter");

            // Act
            string suffix = _routingEngine.GetRouteSuffix(methodInfo);

            // Assert
            suffix.Should().Be("$");
        }
    }
}
