using System.Linq;
using Guardian.Library.Enums;

namespace Guardian.Library.Tokens.Operators {

    /// <summary>
    /// A Token that represents an Operator
    /// </summary>
    public class Operator : Token {
        
        public OperatorTypeEnum Type { get; set; }

        public OperatorMapping Mapping {
            get { return Operators.Mapping.SingleOrDefault(m => m.Type == this.Type); }
        }

        public Operator(OperatorTypeEnum type) {

            Type = type;
        }
    }
}