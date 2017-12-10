using Guardian.Library;
using Guardian.Tests.Mock;
using Guardian.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Target_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_IsNull_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = null;

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_Title_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_NotNull_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_Title_NotNull
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_TooLong_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void Document_Title_NotTooLong_ReturnsNoLengthError()
        {
            // Arrange
            _document = Documents.Document;
            _document.Title = "Test";

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_Title_OfExpectedLength
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Public_Document_RequiresTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_HasTags_And_IsPublic_Or_HasTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
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

            List<Validation> validations = new List<Validation>()
            {
                Validations.Document_HasTags_And_Either_IsPublic_Or_HasTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndTrueOrFalse_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.OrderOfOperations_TrueAndTrueOrFalse_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }


        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrTrue_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.OrderOfOperations_TrueAndFalseOrTrue_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_FalseOrTrueAndTrue_ReturnsError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.OrderOfOperations_FalseOrTrueAndTrue_ReturnsError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }

        [TestMethod]
        public void OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError()
        {
            // Arrange
            _document = Documents.Document;

            List<Validation> validations = new List<Validation>()
            {
                Validations.OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, _validationErrorComparer);
        }
    }
}