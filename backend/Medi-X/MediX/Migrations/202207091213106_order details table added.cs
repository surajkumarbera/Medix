namespace MediX.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class orderdetailstableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OrderId = c.Int(nullable: false),
                    ProductsId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Orders_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Orders_Id, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: false)
                .Index(t => t.ProductsId)
                .Index(t => t.Orders_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Orders_Id", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Orders_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductsId" });
            DropTable("dbo.OrderDetails");
        }
    }
}
