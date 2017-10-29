using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Guardian.Common.Interfaces;
using Guardian.Data;
using Guardian.Website.EntityFramework;
using Guardian.Website.EntityFramework.Entities;

namespace Guardian.Website.Guardian
{
    internal class ApplicationValidationDataProvider : GuardianDataProvider
    {
        private readonly ApplicationDbContext _ctx;
        private DbContextTransaction _trx;

        public ApplicationValidationDataProvider(Func<ApplicationDbContext> dbContextFactory)
        {
            _ctx = dbContextFactory();
        }

        public override IEnumerable<IValidation> GetValidations(string applicationID)
        {
            return _ctx.Validations
                .Where(r => r.ActiveFlag)
                .Where(r => r.ApplicationID == applicationID)
                .AsNoTracking()
                .ToList();
        }

        public override IEnumerable<IValidationCondition> GetValidationConditions(string applicationID)
        {
            return _ctx.ValidationConditions
                .Where(r => r.ActiveFlag)
                .Where(r => r.ApplicationID == applicationID)
                .AsNoTracking()
                .ToArray();
        }


        // Implement these interfaces
        // Add transactions to the tests
        public override IValidation CreateValidation(IValidation validation)
        {
            ValidationEntity newValidationEntity = new ValidationEntity().MapFromInterface(validation);
            newValidationEntity = _ctx.Validations.Add(newValidationEntity);
            _ctx.SaveChanges();

            return newValidationEntity;
        }

        public override IValidation UpdateValidation(IValidation validation)
        {
            ValidationEntity validationEntity = getValidation(validation.ValidationID);

            validationEntity.ApplicationID = validation.ApplicationID;
            validationEntity.ActiveFlag = ((ValidationEntity)validation).ActiveFlag;
            validationEntity.DateModifiedOffset = DateTimeOffset.UtcNow;
            validationEntity.ErrorCode = validation.ErrorCode;
            validationEntity.ErrorMessage = validation.ErrorMessage;
            validationEntity.Expression = validation.Expression;

            _ctx.SaveChanges();

            return validationEntity;
        }

        public override void DeleteValidation(int validationID)
        {
            ValidationEntity validationEntity = getValidation(validationID);

            _ctx.Validations.Remove(validationEntity);
            _ctx.SaveChanges();
        }

        public override IValidation GetValidation(int validationID)
        {
            return getValidation(validationID);
        }

        public override IValidationCondition CreateValidationCondition(IValidationCondition validationCondition)
        {
            ValidationConditionEntity validationConditionEntity = _ctx.ValidationConditions.Add((ValidationConditionEntity)validationCondition);
            _ctx.SaveChanges();

            return validationConditionEntity;
        }

        public override IValidationCondition UpdateValidationCondition(IValidationCondition validationCondition)
        {
            ValidationConditionEntity validationConditionEntity = getValidationCondition(validationCondition.ValidationConditionID);

            validationConditionEntity.ActiveFlag = ((ValidationConditionEntity)validationCondition).ActiveFlag;
            validationConditionEntity.ApplicationID = validationCondition.ApplicationID;
            validationConditionEntity.DateModifiedOffset = DateTimeOffset.UtcNow;;
            validationConditionEntity.Expression = validationCondition.Expression;

            _ctx.SaveChanges();

            return validationConditionEntity;
        }

        public override void DeleteValidationCondition(IValidationCondition validationCondition)
        {
            ValidationConditionEntity validationConditionEntity = getValidationCondition(validationCondition.ValidationConditionID);

            _ctx.ValidationConditions.Remove(validationConditionEntity);
            _ctx.SaveChanges();
        }

        public override IValidationCondition GetValidationCondition(int validationConditionID)
        {
            return getValidationCondition(validationConditionID);
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

        private ValidationConditionEntity getValidationCondition(int validationConditionID)
        {
            return _ctx.ValidationConditions
                .Where(r => r.ValidationConditionID == validationConditionID)
                .SingleOrDefault();
        }

        private ValidationEntity getValidation(int validationID)
        {
            return _ctx.Validations
                .Where(r => r.ValidationID == validationID)
                .SingleOrDefault();
        }
    }
}