using Guardian.Web.Abstractions;

namespace Guardian.Web.Routing.Responses.Interfaces
{
    internal interface IResponse
    {
        string ContentType { get; }
        void Execute(GuardianContext context);
    }
}