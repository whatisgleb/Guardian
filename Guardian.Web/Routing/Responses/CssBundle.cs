using System.Collections.Generic;

namespace Guardian.Web.Routing.Responses
{
    internal class CssBundle : ResourceBundle
    {
        public CssBundle(IEnumerable<string> resourceNames) : base(resourceNames, "text/css") { }
        internal override string getResourcePath(string assemblyName, string resourceName)
        {
            return $"{assemblyName}.Content.css.{resourceName}";
        }
    }
}