using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library;

namespace Guardian.Tests.Utilities
{
    public class ValidationErrorComparer : IComparer, IComparer<ValidationError>
    {
        public int Compare(ValidationError x, ValidationError y)
        {
            return string.Equals(x.Key, y.Key) && string.Equals(x.ErrorMessage, y.ErrorMessage) ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            ValidationError xError = (ValidationError) x;
            ValidationError yError = (ValidationError) y;

            return Compare(xError, yError);
        }
    }
}