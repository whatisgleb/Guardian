using System;
using Guardian.Common.Interfaces;

namespace Guardian.Data.Tests.EntityFramework.Entities
{
    internal class RuleGroupEntity : IRuleGroup
    {
        public int RuleGroupID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }
    }
}
