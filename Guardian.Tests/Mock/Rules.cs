using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Tests.Mock
{
    public static class Rules
    {
        /// <summary>
        /// This throws an error - cannot pass null into DynamicExpression
        /// </summary>
        public static Rule Document_IsNull = new Rule()
        {
            ID = 1,
            Expression = "Document == null",
            ParameterName = "Document"
        };

        public static Rule Document_Title_IsNull = new Rule()
        {
            ID = 2,
            Expression = "string.IsNullOrWhiteSpace(Document.Title)",
            ParameterName = "Document"
        };

        public static Rule Document_Title_GreaterThanLength = new Rule()
        {
            ID = 3,
            Expression = "Document.Title.Length > 10",
            ParameterName = "Document"
        };

        public static Rule Document_IsPublic = new Rule()
        {
            ID = 4,
            Expression = "Document.Tags.Any(it.Text == \"Public\")",
            ParameterName = "Document"
        };

        public static Rule Document_Tags_IsNull = new Rule()
        {
            ID = 5,
            Expression = "Document.Tags == null",
            ParameterName = "Document"
        };

        public static Rule True = new Rule()
        {
            ID = 100,
            Expression = "True",
            ParameterName = "Document"
        };

        public static Rule False = new Rule()
        {
            ID = 101,
            Expression = "False",
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