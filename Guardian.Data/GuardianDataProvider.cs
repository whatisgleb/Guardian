using System.Collections.Generic;
using Guardian.Common.Interfaces;

namespace Guardian.Data
{
    public abstract class GuardianDataProvider
    {
        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();

        public abstract IRuleGroup CreateRuleGroup(IRuleGroup ruleGroup);
        public abstract IRuleGroup UpdateRuleGroup(IRuleGroup ruleGroup);
        public abstract void DeleteRuleGroup(int ruleGroupID);
        public abstract IRuleGroup GetRuleGroup(int ruleGroupID);
        public abstract IEnumerable<IRuleGroup> GetRuleGroups(string applicationID);


        public abstract IRule CreateRule(IRule rule);
        public abstract IRule UpdateRule(IRule rule);
        public abstract void DeleteRule(IRule rule);
        public abstract IRule GetRule(int ruleID);
        public abstract IEnumerable<IRule> GetRules(string applicationID);
    }
}