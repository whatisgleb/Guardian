using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Controllers;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;
using Newtonsoft.Json;

namespace Guardian.Web.Routing
{
    public class RouteHandler
    {
        private readonly Type _controllerType;
        private readonly MethodInfo _methodInfo;

        public RouteHandler(Type controllerType, MethodInfo methodInfo)
        {
            _controllerType = controllerType;
            _methodInfo = methodInfo;
        }

        public Task HandleRequest(GuardianOwinContext context)
        {
            object controllerInstance = getControllerInstance();

            List<object> methodParameters = new List<object>();
            List<Type> expectedParameterTypes = _methodInfo.GetParameters()
                .Select(p => p.ParameterType)
                .ToList();

            if (expectedParameterTypes.Any())
            {
                string json = getRequestBodyJson(context);
                object parameter = JsonConvert.DeserializeObject(json, expectedParameterTypes.First());
                methodParameters.Add(parameter);
            }

            IResponse response = (IResponse)_methodInfo.Invoke(controllerInstance, methodParameters.ToArray());

            return Task.Factory.StartNew(() => response.Execute(context));
        }

        private object getControllerInstance()
        {
            object[] controllerConstructorParameters = new object[] { };
            return Activator.CreateInstance(_controllerType, controllerConstructorParameters);
        }

        private string getRequestBodyJson(GuardianOwinContext context)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                context.Request.Body.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();

                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}

