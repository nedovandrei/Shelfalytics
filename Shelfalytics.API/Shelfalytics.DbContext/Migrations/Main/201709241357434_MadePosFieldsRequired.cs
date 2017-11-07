namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePosFieldsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PointOfSales", "City", c => c.String(nullable: false));
            AlterColumn("dbo.PointOfSales", "TradeChannel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PointOfSales", "TradeChannel", c => c.String());
            AlterColumn("dbo.PointOfSales", "City", c => c.String());
        }
    }
}
