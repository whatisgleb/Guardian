using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Tests.Mock
{
    public static class Rules {
        public static Rule TitleIsEmpty = new Rule() {
            ID = 1,
            Expression = "string.IsNullOrWhiteSpace(Document.Title)",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };
        public static Rule UploadedState = new Rule() {
            ID = 2,
            Expression = "Document.State.ID == 1",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };
        public static Rule PublicTag = new Rule()
        {
            ID = 3,
            Expression = "Document.Tags.Any(ID == 2)",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };
        public static Rule PublicTagAvailable = new Rule()
        {
            ID = 4,
            Expression = "!Document.Tags.Any(it.State.ID == 1)",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };
        public static Rule DocumentTitleContains = new Rule() {

            ID = 5,
            Expression = "Document.Title.Contains(\"test\")",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };
        public static Rule DocumentTitleNotNull = new Rule()
        {
            ID = 6,
            Expression = "!string.IsNullOrEmpty(Document.Title)",
            ApplicationName = "Demo",
            ParameterName = "Document"
        };

        public static List<Rule> All
            =>
                typeof(Rules).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(Rule))
                    .Select(f => (Rule) f.GetValue(null))
                    .ToList();
    }
}
