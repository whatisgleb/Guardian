﻿namespace Guardian {
    public class Identifier : Token {
        
        public int ID { get; set; }

        public Identifier(int id) {

            ID = id;
        }
    }
}