using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;

namespace Guardian.Web.Implementations
{
    internal class ValidationCondition : IValidationCondition
    {
        public int ValidationConditionID { get; set; }
        public string Expression { get; set; }
        public string ApplicationID { get; set; }
    }
}
