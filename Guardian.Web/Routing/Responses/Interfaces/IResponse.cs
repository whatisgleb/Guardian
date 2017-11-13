using System.IO;
using Guardian.Web.Owin;

namespace Guardian.Web.Routing.Responses.Interfaces
{
    internal interface IResponse
    {
        string ContentType { get; }
        void Execute(GuardianOwinContext context);
    }
}