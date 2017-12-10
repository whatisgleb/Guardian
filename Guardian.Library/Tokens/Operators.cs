using Guardian.Library.Interfaces;
using System.Collections.Generic;

namespace Guardian.Library.Tokens
{
    public static class Operators
    {
        public static AndOperator And = new AndOperator();
        public static OrOperator Or = new OrOperator();
        public static NotOperator Not = new NotOperator();
        public static OpenParanthesisGroupingOperator OpenParanthesis = new OpenParanthesisGroupingOperator();
        public static CloseParanthesisGroupingOperator CloseParanthesis = new CloseParanthesisGroupingOperator();

        public static IDictionary<string, IOperator> All = new Dictionary<string, IOperator>()
        {
            { And.StringRepresentation, And },
            { Or.StringRepresentation, Or },
            { Not.StringRepresentation, Not },
            { OpenParanthesis.StringRepresentation, OpenParanthesis },
            { CloseParanthesis.StringRepresentation, CloseParanthesis }
        };
    }
}