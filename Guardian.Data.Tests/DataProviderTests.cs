﻿using System;
using FluentAssertions;
using Guardian.Data.Tests.EntityFramework;
using Guardian.Data.Tests.EntityFramework.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Guardian.Data.Tests
{
    [TestClass]
    public class DataProviderTests
    {
        private GuardianDataProvider _dataProvider;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            GuardianDataProviderConfiguration.RegisterDataProviderFactory(() => new TestDataProvider(() => new TestDbContext()));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _dataProvider = GuardianDataProviderConfiguration.GetDataProvider();
            _dataProvider.BeginTransaction();
        }

        [TestCleanup]
        public void TestDestroy()
        {
            _dataProvider.RollbackTransaction();
        }

        [TestMethod]
        public void Registration_Persists()
        {
            // Assert
            _dataProvider.Should().NotBeNull();
        }

        [TestMethod]
        public void Create_Get_Update_Validation_Succeeds()
        {
            // Arrange
            ValidationEntity validationEntity = new ValidationEntity
            {
                ActiveFlag = true,
                ApplicationID = "Guardian",
                DateCreatedOffset = DateTimeOffset.UtcNow,
                DateModifiedOffset = DateTimeOffset.UtcNow,
                ErrorCode = 0,
                ErrorMessage = "There was a problem",
                Expression = "1"
            };

            // Act
            _dataProvider.CreateValidation(validationEntity);

            ValidationEntity createdValidationEntity = 
                (ValidationEntity)_dataProvider.GetValidation(validationEntity.ValidationID);

            createdValidationEntity.ErrorMessage = "There was another problem";

            _dataProvider.UpdateValidation(createdValidationEntity);

            ValidationEntity updatedValidationEntity =
                (ValidationEntity) _dataProvider.GetValidation(createdValidationEntity.ValidationID);

            // Assert
            updatedValidationEntity.Should().BeEquivalentTo(createdValidationEntity);
        }

        [TestMethod]
        public void Create_Get_Update_ValidationCondition_Succeeds()
        {
            // Arrange
            ValidationConditionEntity validationConditionEntity = new ValidationConditionEntity
            {
                ActiveFlag = true,
                ApplicationID = "Guardian",
                DateCreatedOffset = DateTimeOffset.UtcNow,
                DateModifiedOffset = DateTimeOffset.UtcNow,
                Expression = "1"
            };

            // Act
            _dataProvider.CreateValidationCondition(validationConditionEntity);

            ValidationConditionEntity createdValidationConditionEntity =
                (ValidationConditionEntity)_dataProvider.GetValidationCondition(validationConditionEntity.ValidationConditionID);

            createdValidationConditionEntity.Expression = "2";

            _dataProvider.UpdateValidationCondition(createdValidationConditionEntity);

            ValidationConditionEntity updatedValidationConditionEntity =
                (ValidationConditionEntity)_dataProvider.GetValidationCondition(createdValidationConditionEntity.ValidationConditionID);

            // Assert
            updatedValidationConditionEntity.Should().Be(createdValidationConditionEntity);
        }
    }
}
