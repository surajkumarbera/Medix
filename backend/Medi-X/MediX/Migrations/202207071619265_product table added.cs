namespace MediX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producttableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        CategoriesId = c.Int(nullable: false),
                        SubCategoriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoriesId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoriesId, cascadeDelete: true)
                .Index(t => t.CategoriesId)
                .Index(t => t.SubCategoriesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategoriesId", "dbo.SubCategories");
            DropForeignKey("dbo.Products", "CategoriesId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "SubCategoriesId" });
            DropIndex("dbo.Products", new[] { "CategoriesId" });
            DropTable("dbo.Products");
        }
    }
}
