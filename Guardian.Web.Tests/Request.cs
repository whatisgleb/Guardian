using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Web.Abstractions;

namespace Guardian.Web.Tests
{
    internal class Request : GuardianRequest
    {
        private readonly string _path;
        private readonly string _method;

        public Request(string path, string method)
        {
            _path = path;
            _method = method;
        }

        public override string Method => _method;
        public override string Path => _path;
        public override string PathBase { get; }
        public override string LocalIpAddress { get; }
        public override string RemoteIpAddress { get; }
        public override Stream Body { get; }
        public override string GetQuery(string key)
        {
            throw new NotImplementedException();
        }
    }
}
