using System.Linq;
using Guardian.Library.Enums;

namespace Guardian.Library.Tokens.Operators {

    /// <summary>
    /// A Token that represents an OperatorToken
    /// </summary>
    public class OperatorToken : Token {
        
        public OperatorTypeEnum Type { get; set; }

        public OperatorMapping Mapping {
            get { return Operators.Mapping.SingleOrDefault(m => m.Type == this.Type); }
        }

        public OperatorToken(OperatorTypeEnum type) {

            Type = type;
        }
    }
}