using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Guardian.Web.Helpers;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing.Responses
{
    // The idea is to be able to get rid of all the code in the controller and let the middleware deal with that
    // This way you can do things like return Json(ValidationDTO) which would be serialized and converted to a stream by the Json:IResponse class
    internal abstract class ResourceBundle : IResponse
    {
        public string ContentType { get; }
        private IEnumerable<string> _resourceNames;

        public ResourceBundle(IEnumerable<string> resourceNames, string resourceContentType)
        {
            _resourceNames = resourceNames;
            ContentType = resourceContentType;
        }

        public void CopyTo(Stream stream)
        {
            Assembly executingAssembly = ReflectionHelper.GetExecutingAssembly();
            string executingAssemblyName = executingAssembly.GetName().Name;

            IEnumerable<string> resourcePaths = _resourceNames
                .Select(r => getResourcePath(executingAssemblyName, r));

            foreach (string resourcePath in resourcePaths)
            {
                using (var inputStream = executingAssembly.GetManifestResourceStream(resourcePath))
                {
                    if (inputStream == null)
                    {
                        throw new ArgumentException($"Resource, '{resourcePath}' not found in assembly {executingAssembly}.");
                    }

                    inputStream.CopyTo(stream);
                }
            }
        }

        internal abstract string getResourcePath(string assemblyName, string resourceName);
    }
}
