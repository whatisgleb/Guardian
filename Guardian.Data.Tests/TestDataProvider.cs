using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Common.Interfaces;
using Guardian.Data.Tests.EntityFramework;
using Guardian.Data.Tests.EntityFramework.Entities;

namespace Guardian.Data.Tests
{
    internal class TestDataProvider : GuardianDataProvider
    {
        private readonly TestDbContext _ctx;
        private DbContextTransaction _trx;

        public TestDataProvider(Func<TestDbContext> dbContextFactory)
        {
            _ctx = dbContextFactory();
        }

        public override IEnumerable<IRuleGroup> GetRuleGroups(string applicationID)
        {
            return _ctx.RuleGroups
                .Where(r => r.ActiveFlag)
                .Where(r => r.ApplicationID == applicationID)
                .AsNoTracking()
                .ToArray();
        }

        public override IEnumerable<IRule> GetRules(string applicationID)
        {
            return _ctx.Rules
                .Where(r => r.ActiveFlag)
                .Where(r => r.ApplicationID == applicationID)
                .AsNoTracking()
                .ToArray();
        }


        // Implement these interfaces
        // Add transactions to the tests
        public override IRuleGroup CreateRuleGroup(IRuleGroup ruleGroup)
        {
            RuleGroupEntity ruleGroupEntity = _ctx.RuleGroups.Add((RuleGroupEntity)ruleGroup);
            _ctx.SaveChanges();

            return ruleGroupEntity;
        }

        public override IRuleGroup UpdateRuleGroup(IRuleGroup ruleGroup)
        {
            RuleGroupEntity ruleGroupEntity = getRuleGroup(ruleGroup.RuleGroupID);

            ruleGroupEntity.ApplicationID = ruleGroup.ApplicationID;
            ruleGroupEntity.ActiveFlag = ((RuleGroupEntity)ruleGroup).ActiveFlag;
            ruleGroupEntity.DateModifiedOffset = new DateTimeOffset(DateTime.UtcNow);
            ruleGroupEntity.ErrorCode = ruleGroup.ErrorCode;
            ruleGroupEntity.ErrorMessage = ruleGroup.ErrorMessage;
            ruleGroupEntity.Expression = ruleGroup.Expression;

            _ctx.SaveChanges();

            return ruleGroupEntity;
        }

        public override void DeleteRuleGroup(int ruleGroupID)
        {
            RuleGroupEntity ruleGroupEntity = getRuleGroup(ruleGroupID);

            _ctx.RuleGroups.Remove(ruleGroupEntity);
            _ctx.SaveChanges();
        }

        public override IRuleGroup GetRuleGroup(int ruleGroupID)
        {
            return getRuleGroup(ruleGroupID);
        }

        public override IRule CreateRule(IRule rule)
        {
            RuleEntity ruleEntity = _ctx.Rules.Add((RuleEntity) rule);
            _ctx.SaveChanges();

            return ruleEntity;
        }

        public override IRule UpdateRule(IRule rule)
        {
            RuleEntity ruleEntity = getRule(rule.RuleID);

            ruleEntity.ActiveFlag = ((RuleEntity) rule).ActiveFlag;
            ruleEntity.ApplicationID = rule.ApplicationID;
            ruleEntity.DateModifiedOffset = new DateTimeOffset(DateTime.UtcNow);
            ruleEntity.Expression = rule.Expression;

            _ctx.SaveChanges();

            return ruleEntity;
        }

        public override void DeleteRule(IRule rule)
        {
            RuleEntity ruleEntity = getRule(rule.RuleID);

            _ctx.Rules.Remove(ruleEntity);
            _ctx.SaveChanges();
        }

        public override IRule GetRule(int ruleID)
        {
            return getRule(ruleID);
        }

        public override void BeginTransaction()
        {
            _trx = _ctx.Database.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            _trx.Commit();
        }

        public override void RollbackTransaction()
        {
            _trx.Rollback();
        }

        private RuleEntity getRule(int ruleID)
        {
            return _ctx.Rules
                .Where(r => r.RuleID == ruleID)
                .SingleOrDefault();
        }

        private RuleGroupEntity getRuleGroup(int ruleGroupID)
        {
            return _ctx.RuleGroups
                .Where(r => r.RuleGroupID == ruleGroupID)
                .SingleOrDefault();
        }
    }
}
