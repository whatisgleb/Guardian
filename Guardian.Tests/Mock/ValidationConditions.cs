using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Tests.Mock
{
    public static class ValidationConditions
    {
        public static ValidationCondition Document_IsNull = new ValidationCondition()
        {
            ValidationConditionID = 1,
            Expression = "Document == null",
            ParameterName = "Document"
        };

        public static ValidationCondition Document_Title_IsNull = new ValidationCondition()
        {
            ValidationConditionID = 2,
            Expression = "string.IsNullOrWhiteSpace(Document.Title)",
            ParameterName = "Document"
        };

        public static ValidationCondition Document_Title_GreaterThanLength = new ValidationCondition()
        {
            ValidationConditionID = 3,
            Expression = "Document.Title.Length > 10",
            ParameterName = "Document"
        };

        public static ValidationCondition Document_IsPublic = new ValidationCondition()
        {
            ValidationConditionID = 4,
            Expression = "Document.Tags.Any(it.Text == \"Public\")",
            ParameterName = "Document"
        };

        public static ValidationCondition Document_Tags_IsNull = new ValidationCondition()
        {
            ValidationConditionID = 5,
            Expression = "Document.Tags == null",
            ParameterName = "Document"
        };

        public static ValidationCondition True = new ValidationCondition()
        {
            ValidationConditionID = 100,
            Expression = "True",
            ParameterName = "Document"
        };

        public static ValidationCondition False = new ValidationCondition()
        {
            ValidationConditionID = 101,
            Expression = "False",
            ParameterName = "Document"
        };

        public static List<ValidationCondition> All
            =>
                typeof(ValidationConditions).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(ValidationCondition))
                    .Select(f => (ValidationCondition) f.GetValue(null))
                    .ToList();
    }
}