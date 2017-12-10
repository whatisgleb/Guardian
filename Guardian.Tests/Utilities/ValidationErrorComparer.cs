using System.Collections;
using System.Collections.Generic;
using Guardian.Core;

namespace Guardian.Tests.Utilities
{
    public class ValidationErrorComparer : IComparer, IComparer<ValidationError>
    {
        public int Compare(ValidationError x, ValidationError y)
        {
            return string.Equals(x.ErrorCode, y.ErrorCode) && string.Equals(x.ErrorMessage, y.ErrorMessage) ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            ValidationError xError = (ValidationError) x;
            ValidationError yError = (ValidationError) y;

            return Compare(xError, yError);
        }
    }
}