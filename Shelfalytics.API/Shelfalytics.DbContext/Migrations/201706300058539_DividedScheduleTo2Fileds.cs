namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DividedScheduleTo2Fileds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointOfSales", "OpeningHour", c => c.DateTime(nullable: false));
            AddColumn("dbo.PointOfSales", "ClosingHour", c => c.DateTime(nullable: false));
            DropColumn("dbo.PointOfSales", "Schedule");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PointOfSales", "Schedule", c => c.String(nullable: false));
            DropColumn("dbo.PointOfSales", "ClosingHour");
            DropColumn("dbo.PointOfSales", "OpeningHour");
        }
    }
}
