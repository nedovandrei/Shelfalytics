namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeRequiredCHainName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PointOfSales", "ChainName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PointOfSales", "ChainName", c => c.String());
        }
    }
}
