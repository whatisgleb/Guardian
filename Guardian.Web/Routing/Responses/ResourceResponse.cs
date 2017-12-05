using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Helpers;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing.Responses
{
    internal class ResourceResponse : IResponse
    {
        private readonly string _resourceName;
        private Dictionary<string, string> _contentTypesByFileExtension = new Dictionary<string, string>()
        {
            { ".js", "text/javascript" },
            { ".css", "text/css" },
            { ".html",  "text/html" },
            { ".map", "application/octet-stream" }
        };

        public string ContentType => _contentTypesByFileExtension[Path.GetExtension(_resourceName)];

        public ResourceResponse(string resourceName)
        {
            _resourceName = resourceName;
        }

        public void Execute(GuardianContext context)
        {
            context.Response.ContentType = ContentType;
            context.Response.SetExpire(DateTimeOffset.UtcNow.AddMinutes(1));

            Assembly executingAssembly = ReflectionHelper.GetExecutingAssembly();
            string executingAssemblyName = executingAssembly.GetName().Name;
            string resourcePath = $"{executingAssemblyName}.Content.app.dist.guardian.resources.{_resourceName}"; //TODO: Some hardcoded magic here.

            using (var inputStream = executingAssembly.GetManifestResourceStream(resourcePath))
            {
                if (inputStream == null)
                {
                    throw new ArgumentException($"Resource, '{resourcePath}' not found in assembly {executingAssembly}.");
                }

                inputStream.CopyTo(context.Response.Body);
            }
        }
    }
}
