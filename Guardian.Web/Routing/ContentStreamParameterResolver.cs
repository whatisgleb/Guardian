using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Guardian.Web.Routing
{
    internal static class ContentStreamParameterResolver
    {
        /// <summary>
        /// Assuming the specified stream contains valid JSON, returns that JSON payload deserialized into the specified Type.
        /// </summary>
        public static object GetTypedParameter(Stream contentStream, Type parameterType)
        {
            if (contentStream == null || contentStream.Length == 0)
            {
                return null;
            }

            return JsonConvert.DeserializeObject(ConvertStreamToString(contentStream), parameterType);
        }

        /// <summary>
        /// Converts the specified Stream into a string assuming a UTF8 encoding.
        /// </summary>
        private static string ConvertStreamToString(Stream contentStream)
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