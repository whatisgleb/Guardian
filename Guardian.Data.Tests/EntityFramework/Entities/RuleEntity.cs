using System;
using Guardian.Common.Interfaces;

namespace Guardian.Data.Tests.EntityFramework.Entities
{
    internal class RuleEntity : IRule
    {
        public static RuleEntity FromInterface(IRule rule)
        {
            return new RuleEntity
            {
                ActiveFlag = true,
                ApplicationID = rule.ApplicationID,
                Expression = rule.Expression,

                DateCreatedOffset = new DateTimeOffset(),
                DateModifiedOffset = new DateTimeOffset()
            };
        }

        public int RuleID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }
    }
}
