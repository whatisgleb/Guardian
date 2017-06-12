using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Tests.Mock {
    public static class RuleGroups {
        /// <summary>
        /// This throws an error - cannot pass null into DynamicExpression
        /// </summary>
        public static RuleGroup Target_NotNull = new RuleGroup() {
            Expression = "!1",
            ErrorMessage = "A Document is required.",
            Key = "Document",
            ParameterType = "Document"
        };

        public static RuleGroup Document_Title_NotNull = new RuleGroup() {
            Expression = "2",
            ErrorMessage = "A Document Title is required.",
            Key = "Document.Title",
            ParameterType = "Document"
        };

        public static RuleGroup Document_Title_OfExpectedLength = new RuleGroup() {
            Expression = "!2 && 3",
            ErrorMessage = "A Document Title is required.",
            Key = "Document.Title",
            ParameterType = "Document"
        };

        public static RuleGroup Public_Document_RequiresTitle = new RuleGroup()
        {
            Expression = "!5 && 4 && 2",
            ErrorMessage = "A Public Document must have a Title.",
            Key = "Document.Title",
            ParameterType = "Document"
        };

        public static RuleGroup Document_HasTags_And_IsPublic_Or_HasTitle = new RuleGroup()
        {
            Expression = "!5 && !4 || !2",
            ErrorMessage = "A Public Document must have a Title.",
            Key = "Document.Title",
            ParameterType = "Document"
        };

        public static RuleGroup Document_HasTags_And_Either_IsPublic_Or_HasTitle = new RuleGroup()
        {
            Expression = "!5 && (4 || !2)",
            ErrorMessage = "A Public Document must have a Title.",
            Key = "Document.Title",
            ParameterType = "Document"
        };

        public static RuleGroup OrderOfOperations_TrueAndTrueOrFalse_ReturnsError = new RuleGroup()
        {
            Expression = "100 && 100 || 101",
            ErrorMessage = "Error",
            Key = "",
            ParameterType = "Document"
        };

        public static RuleGroup OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError = new RuleGroup()
        {
            Expression = "100 && 101 || 101",
            ErrorMessage = "Error",
            Key = "",
            ParameterType = "Document"
        };

        public static RuleGroup OrderOfOperations_TrueAndFalseOrTrue_ReturnsError = new RuleGroup()
        {
            Expression = "100 && 101 || 100",
            ErrorMessage = "Error",
            Key = "",
            ParameterType = "Document"
        };

        public static RuleGroup OrderOfOperations_FalseOrTrueAndTrue_ReturnsError = new RuleGroup()
        {
            Expression = "101 || 100 && 100",
            ErrorMessage = "Error",
            Key = "",
            ParameterType = "Document"
        };

        public static RuleGroup OrderOfOperations_FalseOrTrueAndFalse_ReturnsNoError = new RuleGroup()
        {
            Expression = "101 || 100 && 101",
            ErrorMessage = "Error",
            Key = "",
            ParameterType = "Document"
        };

        public static List<RuleGroup> All => typeof(RuleGroups).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(RuleGroup))
            .Select(f => (RuleGroup) f.GetValue(null))
            .ToList();
    }
}