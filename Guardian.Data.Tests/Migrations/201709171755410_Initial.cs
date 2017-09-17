namespace Guardian.Data.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValidationConditions",
                c => new
                    {
                        ValidationConditionID = c.Int(nullable: false, identity: true),
                        ActiveFlag = c.Boolean(nullable: false),
                        Expression = c.String(),
                        ApplicationID = c.String(),
                        DateCreatedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                        DateModifiedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ValidationConditionID);
            
            CreateTable(
                "dbo.Validations",
                c => new
                    {
                        ValidationID = c.Int(nullable: false, identity: true),
                        ActiveFlag = c.Boolean(nullable: false),
                        Expression = c.String(),
                        ErrorMessage = c.String(),
                        ErrorCode = c.Int(nullable: false),
                        ApplicationID = c.String(),
                        DateCreatedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                        DateModifiedOffset = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ValidationID);
            
            DropTable("dbo.RuleGroups");
            DropTable("dbo.Rules");
        }
        
        public override void Down()
        {
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
            
            DropTable("dbo.Validations");
            DropTable("dbo.ValidationConditions");
        }
    }
}
