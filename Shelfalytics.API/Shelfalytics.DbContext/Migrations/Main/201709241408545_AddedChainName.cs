namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChainName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointOfSales", "ChainName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointOfSales", "ChainName");
        }
    }
}
