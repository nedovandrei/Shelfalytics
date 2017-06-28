namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEquipmentReadingidFromDistanceReading : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EquipmentDistanceReadings", "EquipmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EquipmentDistanceReadings", "EquipmentId", c => c.Int(nullable: false));
        }
    }
}
