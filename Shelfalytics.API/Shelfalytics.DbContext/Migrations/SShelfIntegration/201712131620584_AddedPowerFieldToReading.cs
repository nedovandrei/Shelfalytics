namespace Shelfalytics.DbContext.Migrations.SShelfIntegration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPowerFieldToReading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SShelfEquipmentReadings", "Power", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SShelfEquipmentReadings", "Power");
        }
    }
}
