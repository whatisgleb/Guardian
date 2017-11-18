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
    internal sealed class GuardianOwinResponse : GuardianResponse
    {
        private readonly IOwinContext _context;

        public GuardianOwinResponse(IDictionary<string, object> environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            _context = new OwinContext(environment);
        }

        public override string ContentType {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public override int StatusCode {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }

        public override Stream Body => _context.Response.Body;

        public override void SetExpire(DateTimeOffset? value)
        {
            _context.Response.Expires = value;
        }

        public override Task WriteAsync(string text)
        {
            return _context.Response.WriteAsync(text);
        }
    }
}
