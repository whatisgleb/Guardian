using System.Collections.Generic;
using Guardian.Library.Interfaces;

namespace Guardian.Library.Tokens
{
    public static class Operators
    {
        public static AndOperator And = new AndOperator();
        public static OrOperator Or = new OrOperator();
        public static NotOperator Not = new NotOperator();
        public static OpenParanthesisGroupingOperator OpenParanthesis = new OpenParanthesisGroupingOperator();
        public static CloseParanthesisGroupingOperator CloseParanthesis = new CloseParanthesisGroupingOperator();

        public static List<IOperator> All = new List<IOperator>()
        {
            And,
            Or,
            Not,
            OpenParanthesis,
            CloseParanthesis
        };
    }
}
