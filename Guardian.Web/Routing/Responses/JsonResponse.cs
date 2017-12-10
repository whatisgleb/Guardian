using System;
using System.IO;
using System.Text;
using Guardian.Web.Abstractions;
using Guardian.Web.Routing.Responses.Interfaces;
using Newtonsoft.Json;

namespace Guardian.Web.Routing.Responses
{
    internal class JsonResponse : IResponse
    {
        private readonly object _payload;
        public string ContentType { get; } = "application/json";

        public JsonResponse(object payload)
        {
            _payload = payload;
        }

        public void Execute(GuardianContext context)
        {
            context.Response.ContentType = ContentType;
            context.Response.SetExpire(DateTimeOffset.UtcNow.AddMinutes(1));

            string serialized = JsonConvert.SerializeObject(_payload);
            byte[] bytes = Encoding.UTF8.GetBytes(serialized);
            new MemoryStream(bytes).CopyTo(context.Response.Body);
        }
    }
}
