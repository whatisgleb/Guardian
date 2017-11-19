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

        public ResourceResponse(string resourceName)
        {
            _resourceName = resourceName
                .Replace("/", ".");
        }
        
        private Dictionary<string, string> _contentTypesByFileExtension = new Dictionary<string, string>()
        {
            { ".js", "text/javascript" },
            { ".css", "text/css" },
            { ".html",  "text/html" }
        };

        public string ContentType
        {
            get { return _contentTypesByFileExtension[Path.GetExtension(_resourceName
                .Replace("-", "."))]; }
        }

        public void Execute(GuardianContext context)
        {
            context.Response.ContentType = ContentType;
            context.Response.SetExpire(DateTimeOffset.UtcNow.AddMinutes(1));

            Assembly executingAssembly = ReflectionHelper.GetExecutingAssembly();
            string executingAssemblyName = executingAssembly.GetName().Name;
            string resourcePath = $"{executingAssemblyName}.Content.app.dist.resources.{_resourceName}";

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
