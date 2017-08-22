namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiameterToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "BottleDiameter", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "BottleDiameter");
        }
    }
}
