using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Controllers;
using Guardian.Web.Extensions;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing
{
    /// <summary>
    /// This class orchestrates the pieces that can generate a server response for a given request context.
    /// </summary>
    internal class RouteHandler
    {
        public object[] Parameters { get; }
        public readonly MethodInfo ControllerMethodInfo;

        internal RouteHandler(MethodInfo controllerMethodInfo, object[] parameters = null)
        {
            Parameters = parameters ?? new object[0];
            ControllerMethodInfo = controllerMethodInfo;

            Debug.Assert(Parameters.Count() <= 1, "Guardian Routing supports only one parameter at a time. Route Handler was given a collection of more than one Parameter.");
        }

        /// <summary>
        /// Returns a Task that executes the work that will ultimately respond to the request.
        /// </summary>
        internal Task HandleRequest(GuardianContext context)
        {
            // Instantiate controller and execute the target method with the deserialized parameters
            object controllerInstance = Activator.CreateInstance(ControllerMethodInfo.ReflectedType);
            IResponse response = (IResponse)ControllerMethodInfo.Invoke(controllerInstance, Parameters);

            // Return a Task that will return the actual response to the client
            return Task.Factory.StartNew(() => response.Execute(context));
        }
    }
}

