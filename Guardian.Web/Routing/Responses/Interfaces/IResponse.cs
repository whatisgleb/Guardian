using System.IO;
using Guardian.Web.Abstractions;
using Guardian.Web.Owin;

namespace Guardian.Web.Routing.Responses.Interfaces
{
    internal interface IResponse
    {
        string ContentType { get; }
        void Execute(GuardianContext context);
    }
}