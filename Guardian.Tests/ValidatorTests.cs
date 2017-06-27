﻿using System;
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

        public ValidatorTests() {

            _testServices = new TestServices();
        }

        /// <summary>
        /// Null parent values cannot be passed into DynamicExpression
        /// </summary>
        [TestMethod]
        public void Document_IsNull_ReturnsError()
        {
            // Arrange
            Document document = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Target_NotNull
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_Title_IsNull_ReturnsError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_Title_NotNull
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_Title_NotNull_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_Title_NotNull
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_Title_TooLong_ReturnsError()
        {
            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_Title_NotTooLong_ReturnsNoLengthError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = "Test";

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_Title_IsNull_ReturnsNoLengthError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_Title_OfExpectedLength
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPublicWithNullTitle_ReturnsError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;
            document.Tags = new List<Tag>() {
                
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPrivateWithNullTitle_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;
            document.Tags = new List<Tag>() {

                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsNotTaggedWithNullTitle_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;
            document.Tags = new List<Tag>() {};

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_HasNullTagsWithNullTitle_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPrivateWithTitle_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Tags = new List<Tag>() {

                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPublicWithTitle_ReturnsNoError() {
            // Arrange
            Document document = Documents.Document;
            document.Tags = new List<Tag>() {

                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Public_Document_RequiresTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPrivateWithNullTitle_ReturnsError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;
            document.Tags = new List<Tag>() {

                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_HasTags_And_IsPublic_Or_HasTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void Document_IsPublicWithNullTitle_ReturnsNoError()
        {
            // Arrange
            Document document = Documents.Document;
            document.Title = null;
            document.Tags = new List<Tag>() {

                Tags.PrivateTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Document_HasTags_And_Either_IsPublic_Or_HasTitle
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndTrueOrFalse_ReturnsError() {

            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.OrderOfOperations_TrueAndTrueOrFalse_ReturnsError
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }


        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError()
        {

            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.OrderOfOperations_TrueAndFalseOrFalse_ReturnsNoError
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void OrderOfOperations_TrueAndFalseOrTrue_ReturnsError()
        {

            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.OrderOfOperations_TrueAndFalseOrTrue_ReturnsError
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void OrderOfOperations_FalseOrTrueAndTrue_ReturnsError()
        {

            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.OrderOfOperations_FalseOrTrueAndTrue_ReturnsError
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError()
        {

            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.OrderOfOperations_Not_FalseOrTrueAndTrue_ReturnsNoError
            };

            Validator<Document> validator = new Validator<Document>(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = new List<ValidationError>();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }
    }
}
