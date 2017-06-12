using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Library.Tokens.Values
{
    public class ValueToken : Token 
    {
        public bool Value { get; set; }

        public ValueToken(bool value) {

            this.Value = value;
        }
    }
}
