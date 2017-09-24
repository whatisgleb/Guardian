using System;
using System.IO;
using Guardian.Web.Helpers;
using Guardian.Web.Routing.Responses.Interfaces;

namespace Guardian.Web.Routing.Responses
{
    internal class Page : IResponse
    {
        private readonly string _name;
        public string ContentType { get; } = "text/html";

        public Page(string name)
        {
            _name = name;
        }

        public void CopyTo(Stream stream)
        {
            var executingAssembly = ReflectionHelper.GetExecutingAssembly();
            var pagePath = $"{executingAssembly.GetName().Name}.Pages.{_name}.html";

            using (var inputStream = executingAssembly.GetManifestResourceStream(pagePath))
            {
                if (inputStream == null)
                {
                    throw new ArgumentException($"Page, '{_name}' not found in assembly {executingAssembly}.");
                }

                inputStream.CopyTo(stream);
            }
        }
    }
}