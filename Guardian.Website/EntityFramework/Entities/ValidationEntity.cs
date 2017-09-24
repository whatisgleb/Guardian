using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guardian.Common.Interfaces;

namespace Guardian.Website.EntityFramework.Entities
{
    public class ValidationEntity : IValidation
    {
        public ValidationEntity()
        {
            DateCreatedOffset = DateTimeOffset.UtcNow;
        }

        public int ValidationID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }

        public ValidationEntity MapFromInterface(IValidation validation)
        {
            this.ApplicationID = validation.ApplicationID;
            this.DateModifiedOffset = DateTimeOffset.UtcNow;
            this.ErrorCode = validation.ErrorCode;
            this.ErrorMessage = validation.ErrorMessage;
            this.Expression = validation.Expression;

            return this;
        }
    }
}