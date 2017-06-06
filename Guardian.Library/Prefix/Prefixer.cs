using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library.Enums;
using Guardian.Library.Interfaces;
using Guardian.Library.Tokens;
using Guardian.Library.Tokens.Identifiers;
using Guardian.Library.Tokens.Operators;

namespace Guardian.Library.Prefix
{
    public class Prefixer : IPrefixConverter
    {
        private readonly ITokenParser _tokenParser;

        public Prefixer(ITokenParser tokenParser) {
            
            _tokenParser = tokenParser;
        }

        public Stack<Token> ConvertToStack(string expression) {

            Stack<Token> output = new Stack<Token>();

            // Implement http://scanftree.com/Data_Structure/infix-to-prefix

            return output;
        }
    }
}
