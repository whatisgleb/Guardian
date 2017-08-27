using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            GuardianDataConfiguration.RegisterDataProvider(() => new TestDataProvider(() => new TestDbContext()));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _dataProvider = GuardianDataConfiguration.GetDataProvider();
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
            Assert.IsTrue(_dataProvider != null);
        }

        [TestMethod]
        public void Create_Get_Update_RuleGroup_Succeeds()
        {
            // Arrange
            RuleGroupEntity ruleGroupEntity = new RuleGroupEntity
            {
                ActiveFlag = true,
                ApplicationID = "Guardian",
                DateCreatedOffset = new DateTimeOffset(DateTime.UtcNow),
                DateModifiedOffset = new DateTimeOffset(DateTime.UtcNow),
                ErrorCode = 0,
                ErrorMessage = "There was a problem",
                Expression = "1"
            };

            // Act
            _dataProvider.CreateRuleGroup(ruleGroupEntity);

            RuleGroupEntity createdRuleGroupEntity = 
                (RuleGroupEntity)_dataProvider.GetRuleGroup(ruleGroupEntity.RuleGroupID);

            createdRuleGroupEntity.ErrorMessage = "There was another problem";

            _dataProvider.UpdateRuleGroup(createdRuleGroupEntity);

            RuleGroupEntity updatedRuleGroupEntity =
                (RuleGroupEntity) _dataProvider.GetRuleGroup(createdRuleGroupEntity.RuleGroupID);

            // Assert

            Assert.AreEqual(0,
                new EntityComparer<RuleGroupEntity>().Compare(createdRuleGroupEntity, updatedRuleGroupEntity));
        }

        [TestMethod]
        public void Create_Get_Update_Rule_Succeeds()
        {
            // Arrange
            RuleEntity ruleEntity = new RuleEntity
            {
                ActiveFlag = true,
                ApplicationID = "Guardian",
                DateCreatedOffset = new DateTimeOffset(DateTime.UtcNow),
                DateModifiedOffset = new DateTimeOffset(DateTime.UtcNow),
                Expression = "1"
            };

            // Act
            _dataProvider.CreateRule(ruleEntity);

            RuleEntity createdRuleEntity =
                (RuleEntity)_dataProvider.GetRule(ruleEntity.RuleID);

            createdRuleEntity.Expression = "2";

            _dataProvider.UpdateRule(createdRuleEntity);

            RuleEntity updatedRuleEntity =
                (RuleEntity)_dataProvider.GetRule(createdRuleEntity.RuleID);

            // Assert
            Assert.AreEqual(0,
                new EntityComparer<RuleEntity>().Compare(createdRuleEntity, updatedRuleEntity));
        }
    }
}
