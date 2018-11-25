using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Guardian.Core.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Core.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        private Document _document;
        private Validator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _document = Documents.Document;

            _validator = new Validator();
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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
            
            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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
            
            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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
            
            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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
            
            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = validations
                .Select(r => r.ToValidationError())
                .ToList();

            results.Should().BeEquivalentTo(expectedResults);
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

            // Act
            List<ValidationError> results = _validator.Validate(_document, validations, ValidationConditions.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();

            results.Should().BeEquivalentTo(expectedResults);
        }
    }
}