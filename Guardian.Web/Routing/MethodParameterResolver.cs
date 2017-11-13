using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Guardian.Web.Routing
{
    internal static class MethodParameterResolver
    {
        internal static object GetMethodParameter(MethodInfo methodInfo, Stream contentStream)
        {
            List<Type> expectedParameterTypes = methodInfo.GetParameters()
                .Select(p => p.ParameterType)
                .ToList();

            if (expectedParameterTypes.Count == 0)
            {
                return null;
            }

            if (expectedParameterTypes.Count > 1)
            {
                throw new ArgumentException($"The target method expects {expectedParameterTypes.Count} paremeters. This is not currently supported by the {nameof(GuardianRouter)}");
            }

            Type parameterType = expectedParameterTypes.First();
            string json = GetRequestBodyJson(contentStream);
            return JsonConvert.DeserializeObject(json, parameterType);
        }

        private static string GetRequestBodyJson(Stream contentStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                contentStream.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();

                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}