using Guardian.Library.Enums;

namespace Guardian.Library.Tokens.Operators {
    /// <summary>
    /// An object that represents how an Operator Token should appear in a string
    /// </summary>
    public class OperatorMapping
    {
        public int RequiredConsecutiveCharacterCount { get; set; }
        public char Character { get; set; }
        public OperatorTypeEnum Type { get; set; }

        public string StringRepresentation => new string(this.Character, this.RequiredConsecutiveCharacterCount);

        public OperatorMapping(OperatorTypeEnum type, char character, int requiredConsecutiveCharacterCount)
        {

            Type = type;
            Character = character;
            RequiredConsecutiveCharacterCount = requiredConsecutiveCharacterCount;
        }
    }
}