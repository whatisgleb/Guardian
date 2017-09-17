using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Guardian.Tests.Mock
{
    public static class Validations
    {
        /// <summary>
        /// This throws an error - cannot pass null into DynamicExpression
        /// </summary>
        public static Validation Target_NotNull = new Validation()
        {
            Expression = "1",
            ErrorMessage = "A Document is required.",
            ErrorCode = ErrorCodes.Document
        };

        public static Validation Document_Title_NotNull = new Validation()
        {
            Expression = "2",
            ErrorMessage = "A Document Title is required.",
            ErrorCode = ErrorCodes.DocumentTitle
        };

        public static Validation Document_Title_OfExpectedLength = new Validation()
        {
            Expression = "!2 && 3",
            ErrorMessage = "A Document Title is required.",
            ErrorCode = ErrorCodes.DocumentTitle
        };

        public static Validation Public_Document_RequiresTitle = new Validation()
        {
            Expression = "!5 && 4 && 2",
            ErrorMessage = "A Public Document must have a Title.",
            ErrorCode = ErrorCodes.DocumentTitle
        };

        public static Validation Document_HasTags_And_IsPublic_Or_HasTitle = new Validation()
        {
            Expression = "!5 && !4 || !2",
            ErrorMessage = "A Public Document must have a Title.",
            ErrorCode = ErrorCodes.DocumentTitle
        };

        public static Validation Document_HasTags_And_Either_IsPublic_Or_HasTitle = new Validation()
        {
            Expression = "!5 && (4 || !2)",
            ErrorMessage = "A Public Document must have a Title.",
            ErrorCode = ErrorCodes.DocumentTitle
        };

        public static Validation OrderOfOperations_TrueAndTrueOrFalse_ReturnsError = new Validation()
        {
            Expression = "100 && 100 || 101",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static Validation OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError = new Validation()
        {
            Expression = "100 && 101 || 101",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static Validation OrderOfOperations_TrueAndFalseOrTrue_ReturnsError = new Validation()
        {
            Expression = "100 && 101 || 100",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static Validation OrderOfOperations_FalseOrTrueAndTrue_ReturnsError = new Validation()
        {
            Expression = "101 || 100 && 100",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static Validation OrderOfOperations_FalseOrTrueAndFalse_ReturnsNoError = new Validation()
        {
            Expression = "101 || 100 && 101",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static Validation OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError = new Validation()
        {
            Expression = "!(101 || 100 && 100)",
            ErrorMessage = "Error",
            ErrorCode = ErrorCodes.Unknown
        };

        public static List<Validation> All => typeof(Validations).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Validation))
            .Select(f => (Validation) f.GetValue(null))
            .ToList();
    }
}