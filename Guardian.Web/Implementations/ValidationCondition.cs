using Guardian.Common.Interfaces;

namespace Guardian.Web.Implementations
{
    internal class ValidationCondition : IValidationCondition
    {
        public int ValidationConditionID { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }
    }
}
