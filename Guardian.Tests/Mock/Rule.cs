using Guardian.Library;
using Guardian.Library.Interfaces;

namespace Guardian.Tests.Mock {
    public class Rule : IRule {

        public int ID { get; set; }
        public string ApplicationName { get; set; }
        public string ParameterName { get; set; }
        public string Expression { get; set; }
    }
}