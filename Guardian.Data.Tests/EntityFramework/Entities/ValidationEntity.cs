using System;
using Guardian.Common.Interfaces;

namespace Guardian.Data.Tests.EntityFramework.Entities
{
    internal class ValidationEntity : IValidation
    {
        public int ValidationID { get; set; }
        public bool ActiveFlag { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string ApplicationID { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
        public DateTimeOffset DateModifiedOffset { get; set; }
    }
}
