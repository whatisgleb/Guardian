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
        [TestMethod]
        public void CollectionComplexObjectAccess()
        {

            Document document = Documents.Document;
            document.State = States.State;
            document.Tags = new List<Tag>() {
                Tags.FileTag,
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_PublicTag_State
            };

            Validator validator = new Validator();
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);
            List<ValidationError> expectedResults = new List<ValidationError>() {
                RuleGroups.Require_PublicTag_State.ToValidationError()
            };

            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void CollectionAccess()
        {

            Document document = Documents.Document;
            document.State = States.State;
            document.Tags = new List<Tag>() {
                Tags.FileTag,
                Tags.PublicTag
            };

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_PublicTag
            };

            Validator validator = new Validator();
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);
            List<ValidationError> expectedResults = new List<ValidationError>() {
                RuleGroups.Require_PublicTag.ToValidationError()
            };

            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }
        
        [TestMethod]
        public void ComplexObjectAccess() {
            
            Document document = Documents.Document;
            document.State = States.State;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Required_StatusIsUploaded
            };

            Validator validator = new Validator();
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);
            List<ValidationError> expectedResults = new List<ValidationError>() {
                RuleGroups.Required_StatusIsUploaded.ToValidationError()
            };

            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }

        [TestMethod]
        public void PropertyAccess()
        {

            Document document = Documents.Document;

            List<RuleGroup> ruleGroups = new List<RuleGroup>() {
                RuleGroups.Require_NonNullTitle
            };

            Validator validator = new Validator();
            List<ValidationError> results = validator.Validate(document, ruleGroups, Rules.All);
            List<ValidationError> expectedResults = new List<ValidationError>() {
                RuleGroups.Require_NonNullTitle.ToValidationError()
            };

            CollectionAssert.AreEqual(expectedResults, results, new ValidationErrorComparer());
        }
    }
}
