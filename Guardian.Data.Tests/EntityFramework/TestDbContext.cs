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

        public DbSet<RuleGroupEntity> RuleGroups { get; set; }
        public DbSet<RuleEntity> Rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<RuleEntity>()
                .ToTable("Rules")
                .HasKey(r => r.RuleID);

            modelBuilder.Entity<RuleGroupEntity>()
                .ToTable("RuleGroups")
                .HasKey(r => r.RuleGroupID);
        }
    }
}
