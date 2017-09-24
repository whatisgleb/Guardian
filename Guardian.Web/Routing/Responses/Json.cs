using System.IO;
using System.Text;
using Guardian.Web.Routing.Responses.Interfaces;
using Newtonsoft.Json;

namespace Guardian.Web.Routing.Responses
{
    public class Json : IResponse
    {
        private readonly object _payload;
        public string ContentType { get; } = "application/json";

        public Json(object payload)
        {
            _payload = payload;
        }

        public void CopyTo(Stream stream)
        {
            string serialized = JsonConvert.SerializeObject(_payload);
            byte[] bytes = Encoding.UTF8.GetBytes(serialized);
            new MemoryStream(bytes).CopyTo(stream);
        }
    }
}
