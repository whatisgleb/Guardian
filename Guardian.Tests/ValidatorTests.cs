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

        public ValidatorTests() {

            _testServices = new TestServices();
        }

        [TestMethod]
        public void CollectionComplexObjectAccess()
        {
            // Arrange
            Document document = Documents.Document;
            document.State = States.State;
            document.Tags = new List<Tag>() {
                Tags.FileTag,
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_PublicTag_State
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void CollectionAccess()
        {
            // Arrange
            Document document = Documents.Document;
            document.State = States.State;
            document.Tags = new List<Tag>() {
                Tags.FileTag,
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_PublicTag
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }
        
        [TestMethod]
        public void ComplexObjectAccess() {
            
            // Arrange
            Document document = Documents.Document;
            document.State = States.State;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Required_StatusIsUploaded
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void PropertyAccess()
        {
            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_NonNullTitle
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void NullStringPropertyContains()
        {
            // Arrange
            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_TitleContains
            };

            Validator validator = new Validator(_testServices.PostfixConverter);

            // Act
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);

            // Assert
            List<ValidationError> expectedResults = ruleGroups.Select(r => r.ToValidationError()).ToList();
            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }
    }
}
