using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Guardian.Web.Controllers;
using Guardian.Web.Extensions;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing
{
    internal class RouteHandler
    {
        public IEnumerable<object> Parameters { get; }
        public readonly MethodInfo ControllerMethodInfo;

        internal RouteHandler(MethodInfo controllerMethodInfo, IEnumerable<object> parameters)
        {
            Parameters = parameters;
            ControllerMethodInfo = controllerMethodInfo;
        }

        internal Task HandleRequest(GuardianOwinContext context)
        {
            // Instantiate controller and execute the target method with the deserialized parameters
            object controllerInstance = Activator.CreateInstance(ControllerMethodInfo.ReflectedType);
            IResponse response = (IResponse)ControllerMethodInfo.Invoke(controllerInstance, Parameters.ToArray());

            // Return a Task that will return the actual response to the client
            return Task.Factory.StartNew(() => response.Execute(context));
        }
    }
}

