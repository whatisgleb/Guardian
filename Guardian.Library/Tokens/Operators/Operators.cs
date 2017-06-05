using System.Collections.Generic;
using Guardian.Library.Enums;

namespace Guardian.Library.Tokens.Operators
{
    public static class Operators
    {
        /// <summary>
        /// Defines how Operators are represented in a string
        /// </summary>
        public static readonly List<OperatorMapping> Mapping = new List<OperatorMapping>() {

            new OperatorMapping(OperatorTypeEnum.And, '&', 2),
            new OperatorMapping(OperatorTypeEnum.Or, '|', 2),
            new OperatorMapping(OperatorTypeEnum.Not, '!', 1),
            new OperatorMapping(OperatorTypeEnum.OpenParanthesis, '(', 1),
            new OperatorMapping(OperatorTypeEnum.CloseParanthesis, ')', 1)
        };

        /// <summary>
        /// Defines the precedence of Operators. This drives the rules by which Operators are combined into a Postfix Token Stack
        /// </summary>
        public static Dictionary<OperatorTypeEnum, int> Weights = new Dictionary<OperatorTypeEnum, int>() {

            { OperatorTypeEnum.Not, 10 },
            { OperatorTypeEnum.And, 1 },
            { OperatorTypeEnum.Or, 0 }
        };
    }
}
