namespace Guardian {
    public class Operator : Token {
        
        public OperatorTypeEnum Type { get; set; }

        public Operator(OperatorTypeEnum type) {

            Type = type;
        }
    }
}