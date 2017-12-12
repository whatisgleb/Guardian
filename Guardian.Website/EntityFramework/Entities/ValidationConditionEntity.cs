using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guardian.Common.Interfaces;

namespace Guardian.Website.EntityFramework.Entities
{
    public class ValidationConditionEntity : IValidationCondition
    {
        public ValidationConditionEntity()
        {
            ActiveFlag = true;
            this.DateCreatedOffset = DateTimeOffset.UtcNow;
            this.DateModifiedOffset = DateTimeOffset.UtcNow;
        }

        public int ValidationConditionID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }


        public ValidationConditionEntity MapFromInterface(IValidationCondition validationCondition)
        {
            this.ApplicationID = validationCondition.ApplicationID;
            this.Expression = validationCondition.Expression;

            return this;
        }
    }
}