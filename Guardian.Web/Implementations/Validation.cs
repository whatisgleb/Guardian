using Guardian.Common.Interfaces;

namespace Guardian.Web.Implementations
{
    internal class Validation : IValidation
    {
        public int ValidationID { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string ApplicationID { get; set; }
    }
}
