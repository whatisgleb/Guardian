using Guardian.Common.Interfaces;
using Guardian.Library;

namespace Guardian.Tests.Mock
{
    public class RuleGroup : IRuleGroup
    {
        public int RuleGroupID { get; set; }
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