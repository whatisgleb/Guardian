using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Guardian.Data.Tests.EntityFramework.Entities;

namespace Guardian.Data.Tests.EntityFramework
{
    internal class TestDbContext : DbContext
    {
        public TestDbContext() : base(@"Server=(localdb)\v11.0;Initial Catalog=GuardianTestDB;MultipleActiveResultSets=False;")
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
