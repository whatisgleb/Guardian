using System.IO;

namespace Guardian.Web.Abstractions
{
    public abstract class GuardianRequest
    {
        public abstract string Method { get; }
        public abstract string Path { get; }
        public abstract string PathBase { get; }

        public abstract string LocalIpAddress { get; }
        public abstract string RemoteIpAddress { get; }
        public abstract Stream Body { get; }

        public abstract string GetQuery(string key);
    }
}
