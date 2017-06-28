namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToWasOpenedBoolean : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentReadings", "WasOpened", c => c.Boolean(nullable: false));
            DropColumn("dbo.EquipmentReadings", "OpenCloseCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EquipmentReadings", "OpenCloseCount", c => c.Int(nullable: false));
            DropColumn("dbo.EquipmentReadings", "WasOpened");
        }
    }
}
