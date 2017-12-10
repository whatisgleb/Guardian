using Guardian.Common.Interfaces;

namespace Guardian.Core.Tests.Mock
{
    public class Validation : IValidation
    {
        public int ValidationID { get; set; }
        public string ApplicationName { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string ApplicationID { get; set; }

        public ValidationError ToValidationError()
        {
            return new ValidationError()
            {
                ErrorMessage = this.ErrorMessage,
                ErrorCode = this.ErrorCode
            };
        }
    }
}