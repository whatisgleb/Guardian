using Guardian.Web.Abstractions;
using System;
using System.IO;

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
