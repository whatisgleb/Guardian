using System.Collections.Generic;
using Guardian.Common.Interfaces;

namespace Guardian.Data
{
    public abstract class GuardianDataProvider
    {
        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();

        public abstract IValidation CreateValidation(IValidation validation);
        public abstract IValidation UpdateValidation(IValidation validation);
        public abstract void DeleteValidation(int validationID);
        public abstract IValidation GetValidation(int validationID);
        public abstract IEnumerable<IValidation> GetValidations(string applicationID);


        public abstract IValidationCondition CreateValidationCondition(IValidationCondition validationCondition);
        public abstract IValidationCondition UpdateValidationCondition(IValidationCondition validationCondition);
        public abstract void DeleteValidationCondition(IValidationCondition validationCondition);
        public abstract IValidationCondition GetValidationCondition(int validationConditionID);
        public abstract IEnumerable<IValidationCondition> GetValidationConditions(string applicationID);
    }
}