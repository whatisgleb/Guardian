namespace Guardian.Website.Migrations
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Validations");
            DropTable("dbo.ValidationConditions");
        }
    }
}
