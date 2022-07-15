namespace MediX.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ordertableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    PrescriptionsId = c.Int(nullable: false),
                    DateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionsId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.PrescriptionsId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "PrescriptionsId", "dbo.Prescriptions");
            DropIndex("dbo.Orders", new[] { "PrescriptionsId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Orders");
        }
    }
}
