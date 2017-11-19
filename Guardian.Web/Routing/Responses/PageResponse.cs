using System;
using System.IO;
using Guardian.Web.Abstractions;
using Guardian.Web.Helpers;
using Guardian.Web.Owin;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing.Responses
{
    internal class PageResponse : IResponse
    {
        private readonly string _path;
        public string ContentType { get; } = "text/html";

        public PageResponse(string path)
        {
            _path = path;
        }

        public void Execute(GuardianContext context)
        {
            context.Response.ContentType = ContentType;
            context.Response.SetExpire(DateTimeOffset.UtcNow.AddMinutes(1));

            var executingAssembly = ReflectionHelper.GetExecutingAssembly();
            var pagePath = $"{executingAssembly.GetName().Name}.{_path}.html";

            using (var inputStream = executingAssembly.GetManifestResourceStream(pagePath))
            {
                if (inputStream == null)
                {
                    throw new ArgumentException($"Page, '{_path}' not found in assembly {executingAssembly}.");
                }

                inputStream.CopyTo(context.Response.Body);
            }
        }
    }
}