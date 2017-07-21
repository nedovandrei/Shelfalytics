namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatitudeAndLongitude : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointOfSales", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.PointOfSales", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointOfSales", "Longitude");
            DropColumn("dbo.PointOfSales", "Latitude");
        }
    }
}
