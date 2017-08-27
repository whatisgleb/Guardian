using System.Data.Entity.Migrations;

namespace Guardian.Data.Tests.EntityFramework.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RuleGroups",
                c => new
                    {
                        RuleGroupID = c.Int(nullable: false, identity: true),
                        ActiveFlag = c.Boolean(nullable: false),
                        Expression = c.String(),
                        ErrorMessage = c.String(),
                        ErrorCode = c.Int(nullable: false),
                        ApplicationID = c.String(),
                        DateCreatedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                        DateModifiedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.RuleGroupID);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        RuleID = c.Int(nullable: false, identity: true),
                        ActiveFlag = c.Boolean(nullable: false),
                        Expression = c.String(),
                        ApplicationID = c.String(),
                        DateCreatedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                        DateModifiedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.RuleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rules");
            DropTable("dbo.RuleGroups");
        }
    }
}
