using System;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Guardian.Web.Helpers;

namespace Guardian.Web.Controllers
{
    public class GuardianBaseController
    {
        private readonly GuardianContext _context;

        public GuardianBaseController(GuardianContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a resource bundle
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="resourcePath"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        internal Task Resource(string[] resources, string resourcePath, string contentType)
        {
            SetResponseContext(contentType);

            foreach (string resource in resources)
            {
                WriteToResponseBody($"{resourcePath}.{resource.Replace("/", ".")}");
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Returns an HTML page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        internal Task Page(string page)
        {
            SetResponseContext("text/html");
            WriteToResponseBody($"Pages.{page}.html");

            return Task.FromResult(true);
        }

        private void SetResponseContext(string contentType)
        {
            _context.Response.ContentType = contentType;
            _context.Response.SetExpire(DateTimeOffset.Now.AddMinutes(1));
        }

        public void WriteToResponseBody(string path)
        {
            var executingAssembly = ReflectionHelper.GetExecutingAssembly();
            var resourceName = $"{executingAssembly.GetName().Name}.{path}";

            using (var inputStream = executingAssembly.GetManifestResourceStream(resourceName))
            {
                if (inputStream == null)
                {
                    throw new ArgumentException($"Resource, '{path}' not found in assembly {executingAssembly}.");
                }

                inputStream.CopyTo(_context.Response.Body);
            }
        }
    }
}
