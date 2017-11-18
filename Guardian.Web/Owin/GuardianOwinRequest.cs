using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;
using Microsoft.Owin;

namespace Guardian.Web.Owin
{
    internal sealed class GuardianOwinRequest : GuardianRequest
    {
        private readonly IOwinContext _context;

        public GuardianOwinRequest(IDictionary<string, object> environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            _context = new OwinContext(environment);
        }

        public override string Method => _context.Request.Method;
        public override string Path => _context.Request.Path.Value;
        public override string PathBase => _context.Request.PathBase.Value;
        public override string LocalIpAddress => _context.Request.LocalIpAddress;
        public override string RemoteIpAddress => _context.Request.RemoteIpAddress;
        public override Stream Body => _context.Request.Body;

        public override string GetQuery(string key) => _context.Request.Query[key];
    }
}
