using System.Collections.Generic;

namespace Guardian.Core.Tests.Mock
{
    public class Document
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}