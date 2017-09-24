using System.Collections.Generic;

namespace Guardian.Web.Routing.Responses
{
    internal class JsBundle : ResourceBundle
    {
        public JsBundle(IEnumerable<string> resourceNames) : base(resourceNames, "text/javascript") { }
        internal override string getResourcePath(string assemblyName, string resourceName)
        {
            return $"{assemblyName}.Content.scripts.{resourceName}";
        }
    }
}