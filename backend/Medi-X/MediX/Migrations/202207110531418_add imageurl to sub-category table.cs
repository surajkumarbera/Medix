namespace MediX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimageurltosubcategorytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "ImageUrl");
        }
    }
}
