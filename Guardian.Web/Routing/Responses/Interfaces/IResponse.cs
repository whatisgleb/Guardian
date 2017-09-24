using System.IO;

namespace Guardian.Web.Routing.Responses.Interfaces
{
    public interface IResponse
    {
        string ContentType { get; }
        void CopyTo(Stream stream);
    }
}