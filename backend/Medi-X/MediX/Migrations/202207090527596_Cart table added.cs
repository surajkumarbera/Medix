namespace MediX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carttableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "ProductsId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductsId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.Carts");
        }
    }
}
