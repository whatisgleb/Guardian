using System.IO;
using Guardian.Web.Owin;

namespace Guardian.Web.Routing.Responses.Interfaces
{
    public interface IResponse
    {
        string ContentType { get; }
        void Execute(GuardianOwinContext context);
    }
}