namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleToPOS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointOfSales", "Schedule", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointOfSales", "Schedule");
        }
    }
}
