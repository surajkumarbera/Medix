namespace MediX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prescriptionstableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        UserId = c.Int(nullable: false),
                        Verified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "UserId", "dbo.Users");
            DropIndex("dbo.Prescriptions", new[] { "UserId" });
            DropTable("dbo.Prescriptions");
        }
    }
}
