namespace MediX.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class vendoranddescriptionaddedtoproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Vendor", c => c.String());
            AddColumn("dbo.Products", "Descriptions", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "Descriptions");
            DropColumn("dbo.Products", "Vendor");
        }
    }
}
