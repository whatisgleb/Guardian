using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Guardian.Web.Controllers;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing
{
    internal class RouteHandler
    {
        internal readonly Type ControllerType;
        internal readonly MethodInfo ControllerMethodInfo;
        internal readonly string QueryString;

        internal RouteHandler(Type controllerType, MethodInfo controllerMethodInfo, string queryString)
        {
            ControllerType = controllerType;
            ControllerMethodInfo = controllerMethodInfo;
            QueryString = queryString;
        }

        internal Task HandleRequest(GuardianOwinContext context)
        {
            object controllerInstance = GetControllerInstance();
            IEnumerable<object> parameters = GetMethodParameters(context.Request.Body);
            IResponse response = (IResponse)ControllerMethodInfo.Invoke(controllerInstance, parameters.ToArray());

            return Task.Factory.StartNew(() => response.Execute(context));
        }

        internal IDictionary<string, object> GetTypeCastedQueryStringParameters()
        {
            if (string.IsNullOrWhiteSpace(QueryString))
            {
                return new Dictionary<string, object>();
            }

            if (!QueryString.StartsWith("?"))
            {
                return new Dictionary<string, object>()
                {
                    { string.Empty, QueryString }
                };
            }

            IDictionary<string, string> queryStringDictionary = QueryString
                .Replace("?", "")
                .Split('&')
                .Select(s => new
                {
                    ParameterName = s.Split('=')[0],
                    ParameterStringValue = s.Split('=')[1]
                })
                .ToDictionary(v => v.ParameterName, v => v.ParameterStringValue); ;

            IDictionary<string, Type> parameterTypeMap = ControllerMethodInfo.GetParameters()
                .ToDictionary(p => p.Name, p => p.ParameterType);

            Dictionary<string, object> typeCastedParameters = new Dictionary<string, object>();

            foreach (string parameterName in parameterTypeMap.Keys)
            {
                if (queryStringDictionary.ContainsKey(parameterName))
                {
                    Type parameterType = parameterTypeMap[parameterName];
                    string parameterStringValue = queryStringDictionary[parameterName];
                    object parameterValue = Convert.ChangeType(parameterStringValue, parameterType);

                    typeCastedParameters.Add(parameterName, parameterValue);
                }
            }

            return typeCastedParameters;
        }

        internal IEnumerable<object> GetMethodParameters(Stream contentStream)
        {
            List<object> methodParameters = new List<object>();

            if (!string.IsNullOrWhiteSpace(QueryString))
            {
                methodParameters.AddRange(GetTypeCastedQueryStringParameters().Values);
            }
            else if (contentStream.Length > 0)
            {
                methodParameters.Add(MethodParameterResolver.GetMethodParameter(ControllerMethodInfo, contentStream));
            }

            return methodParameters;
        }

        private object GetControllerInstance()
        {
            object[] controllerConstructorParameters = new object[] { };
            return Activator.CreateInstance(ControllerType, controllerConstructorParameters);
        }

    }
}

