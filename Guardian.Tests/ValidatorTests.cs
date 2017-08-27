using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Library;
using Guardian.Tests.Mock;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        private TestServices _testServices;
        private Document _document;
        private ValidationErrorComparer _validationErrorComparer;

        public ValidatorTests()
        {
            _testServices = new TestServices();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _document = Documents.Document;
            _validationErrorComparer = new ValidationErrorComparer();
        }

        /// <summary>
        /// Null parent values cannot be passed into DynamicExpression
        /// </summary>
        [TestMethod]
        public void Document_IsNull_ReturnsError()
        {
            // Arrange
            _document = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Target_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_IsNull_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_Title_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_NotNull_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_Title_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_TooLong_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_NotTooLong_ReturnsNoLengthError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = "Test";

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_IsNull_ReturnsNoLengthError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPublicWithNullTitle_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;
            _document.Tags = new List<Tag>()
            {
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPrivateWithNullTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;
            _document.Tags = new List<Tag>()
            {
                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsNotTaggedWithNullTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;
            _document.Tags = new List<Tag>() {};

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_HasNullTagsWithNullTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPrivateWithTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Tags = new List<Tag>()
            {
                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPublicWithTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Tags = new List<Tag>()
            {
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPrivateWithNullTitle_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;
            _document.Tags = new List<Tag>()
            {
                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_HasTags_And_IsPublic_Or_HasTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_IsPublicWithNullTitle_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;
            _document.Tags = new List<Tag>()
            {
                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.Document_HasTags_And_Either_IsPublic_Or_HasTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndTrueOrFalse_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.OrderOfOperations_TrueAndTrueOrFalse_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }


        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrTrue_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.OrderOfOperations_TrueAndFalseOrTrue_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_FalseOrTrueAndTrue_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.OrderOfOperations_FalseOrTrueAndTrue_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>()
            {
                RuleGroups.OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }
    }
}