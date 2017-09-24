using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Guardian.Website.EntityFramework.Entities;

namespace Guardian.Website.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(@"Server=(localdb)\v11.0;Initial Catalog=GuardianDemoApplication;MultipleActiveResultSets=False;")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ValidationEntity> Validations { get; set; }
        public DbSet<ValidationConditionEntity> ValidationConditions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<ValidationConditionEntity>()
                .ToTable("ValidationConditions")
                .HasKey(r => r.ValidationConditionID);

            modelBuilder.Entity<ValidationEntity>()
                .ToTable("Validations")
                .HasKey(r => r.ValidationID);
        }
    }
}