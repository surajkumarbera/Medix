namespace MediX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subcategorytableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubCategories");
        }
    }
}
