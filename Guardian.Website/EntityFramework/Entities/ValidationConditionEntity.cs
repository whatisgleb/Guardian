using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guardian.Common.Interfaces;

namespace Guardian.Website.EntityFramework.Entities
{
    public class ValidationConditionEntity : IValidationCondition
    {
        public int ValidationConditionID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }
    }
}