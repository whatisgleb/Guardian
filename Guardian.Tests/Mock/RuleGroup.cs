using Guardian.Library;

namespace Guardian.Tests.Mock {
    public class RuleGroup : IRuleGroup {

        public int ID { get; set; }
        public string ApplicationName { get; set; }
        public string ParameterType { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public string Key { get; set; }

        public ValidationError ToValidationError() {
            return new ValidationError() {
                ErrorMessage = this.ErrorMessage,
                Key = this.Key
            };
        }
    }
}