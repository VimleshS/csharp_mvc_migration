namespace mvc_migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "IsSubscribedToNewsletter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "MembershipType_Id", c => c.Byte());
            CreateIndex("dbo.Customers", "MembershipType_Id");
            AddForeignKey("dbo.Customers", "MembershipType_Id", "dbo.MembershipTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MembershipType_Id", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipType_Id" });
            DropColumn("dbo.Customers", "MembershipType_Id");
            DropColumn("dbo.Customers", "MembershipTypeId");
            DropColumn("dbo.Customers", "IsSubscribedToNewsletter");
            DropTable("dbo.MembershipTypes");
        }
    }
}
