namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMissingInfoOnPos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointOfSales", "City", c => c.String());
            AddColumn("dbo.PointOfSales", "TradeChannel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointOfSales", "TradeChannel");
            DropColumn("dbo.PointOfSales", "City");
        }
    }
}
