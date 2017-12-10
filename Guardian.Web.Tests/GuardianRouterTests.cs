using System;
using System.IO;
using System.Text;
using Guardian.Web.Routing;
using Guardian.Web.Routing.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Guardian.Web.Tests
{
    [TestClass]
    public class GuardianRouterTests
    {
        public GuardianRouterTests()
        {
            GuardianRouter.BuildRoutes(typeof(GuardianRouterTests).Assembly);
        }

        [TestMethod]
        public void Given_Request_Router_Finds_ConfiguredRoute()
        {
            // Arrange
            Request request = new Request("/api/testing/router/target/1", HttpRequestMethod.GET);

            // Act
            RouteConfiguration routeConfiguration = GuardianRouter.GetConfiguredRoute(request);

            // Assert
            Assert.IsTrue(routeConfiguration != null);
            Assert.IsTrue(routeConfiguration.Path == @"^/api/testing/router/target/");
            Assert.IsTrue(routeConfiguration.RequestMethod == HttpRequestMethod.GET);
        }

        [TestMethod]
        public void Given_Verb_Mismatch_Request_Router_Finds_NoConfiguredRoute()
        {
            // Arrange
            Request request = new Request("/api/testing/router/target/1", HttpRequestMethod.PUT);

            // Act
            RouteConfiguration routeConfiguration = GuardianRouter.GetConfiguredRoute(request);

            // Assert
            Assert.IsTrue(routeConfiguration == null);
        }

        [TestMethod]
        public void Given_Path_Mismatch_Request_Router_Finds_NoConfiguredRoute()
        {
            // Arrange
            Request request = new Request("/api/testing/router/targets/1", HttpRequestMethod.GET);

            // Act
            RouteConfiguration routeConfiguration = GuardianRouter.GetConfiguredRoute(request);

            // Assert
            Assert.IsTrue(routeConfiguration == null);
        }

        [TestMethod]
        public void Given_Stream_Router_Deserializes_IntoExpectedType()
        {
            // Arrange
            Type objectType = typeof(TestType);
            TestType testObject = new TestType()
            {
                ID = 1,
                Title = "This is a test title"
            };

            string objectJson = JsonConvert.SerializeObject(testObject);
            Byte[] bytes = Encoding.UTF8.GetBytes(objectJson);
            MemoryStream stream = new MemoryStream(bytes);

            // Act
            object deserializedObject = GuardianRouter.GetDeserializedStream(stream, objectType);

            // Assert
            Assert.IsTrue(deserializedObject.GetType() == objectType);
            Assert.IsTrue(testObject.ID == ((TestType)deserializedObject).ID);
            Assert.IsTrue(testObject.Title == ((TestType)deserializedObject).Title);
        }

        [TestMethod]
        public void Given_EmptyStream_Router_Deserializes_IntoNull()
        {
            // Arrange
            Type objectType = typeof(TestType);
            MemoryStream stream = new MemoryStream();

            // Act
            object deserializedObject = GuardianRouter.GetDeserializedStream(stream, objectType);

            // Assert
            Assert.IsTrue(deserializedObject == null);
        }

        [TestMethod]
        public void Given_NullStream_Router_Deserializes_IntoNull()
        {
            // Arrange
            Type objectType = typeof(TestType);

            // Act
            object deserializedObject = GuardianRouter.GetDeserializedStream(null, objectType);

            // Assert
            Assert.IsTrue(deserializedObject == null);
        }

        [TestMethod]
        public void Given_Null_RequestPath_Router_Returns_NullRouteParameter()
        {
            // Arrange
            string requestPath = null;
            string pathPattern = @"^/api/testing/router";
            Type targetType = typeof(string);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNull(pathParameter);
        }

        [TestMethod]
        public void Given_Null_PathPattern_Router_Returns_NullRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router";
            string pathPattern = null;
            Type targetType = typeof(string);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNull(pathParameter);
        }

        [TestMethod]
        public void Given_StringPathParameter_Router_Returns_TypedRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router/string test parameter";
            string pathPattern = "^/api/testing/router/";
            Type targetType = typeof(string);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNotNull(pathParameter);
            Assert.IsTrue(pathParameter.GetType() == targetType);
            Assert.IsTrue(((string)pathParameter) == "string test parameter");
        }

        [TestMethod]
        public void Given_IntPathParameter_Router_Returns_TypedRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router/123";
            string pathPattern = "^/api/testing/router/";
            Type targetType = typeof(int);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNotNull(pathParameter);
            Assert.IsTrue(pathParameter.GetType() == targetType);
            Assert.IsTrue(((int)pathParameter) == 123);
        }

        [TestMethod]
        public void Given_NoIntPathParameter_Router_Returns_DefaultTypedRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router/";
            string pathPattern = "^/api/testing/router/";
            Type targetType = typeof(int);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNotNull(pathParameter);
            Assert.IsTrue(pathParameter.GetType() == targetType);
            Assert.IsTrue(((int)pathParameter) == 0);
        }

        [TestMethod]
        public void Given_NoStringPathParameter_Router_Returns_DefaultTypedRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router/";
            string pathPattern = "^/api/testing/router/";
            Type targetType = typeof(string);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNull(pathParameter);
        }

        [TestMethod]
        public void Given_NoComplexTypePathParameter_Router_Returns_DefaultTypedRouteParameter()
        {
            // Arrange
            string requestPath = "/api/testing/router/";
            string pathPattern = "^/api/testing/router/";
            Type targetType = typeof(TestType);

            // Act
            object pathParameter = GuardianRouter.GetTypedRouteParameter(requestPath, pathPattern, targetType);

            // Assert
            Assert.IsNull(pathParameter);
        }
    }
}
