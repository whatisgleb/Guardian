using Guardian.Common.Interfaces;
using Guardian.Library;

namespace Guardian.Tests.Mock
{
    public class Rule : IRule
    {
        public int RuleID { get; set; }
        public string ApplicationName { get; set; }
        public string ParameterName { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }
    }
}