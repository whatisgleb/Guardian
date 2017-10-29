using System;
using Guardian.Common.Interfaces;

namespace Guardian.Data.Tests.EntityFramework.Entities
{
    internal class ValidationConditionEntity : IValidationCondition
    {
        public static ValidationConditionEntity FromInterface(IValidationCondition validationCondition)
        {
            return new ValidationConditionEntity
            {
                ActiveFlag = true,
                ApplicationID = validationCondition.ApplicationID,
                Expression = validationCondition.Expression,

                DateCreatedOffset = DateTimeOffset.UtcNow,
                DateModifiedOffset = DateTimeOffset.UtcNow
            };
        }

        public int ValidationConditionID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }
    }
}
