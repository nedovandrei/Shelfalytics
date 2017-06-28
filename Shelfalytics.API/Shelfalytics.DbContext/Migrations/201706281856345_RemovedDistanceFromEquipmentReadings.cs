namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDistanceFromEquipmentReadings : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EquipmentReadings", "Distance1");
            DropColumn("dbo.EquipmentReadings", "Distance2");
            DropColumn("dbo.EquipmentReadings", "Distance3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EquipmentReadings", "Distance3", c => c.Int(nullable: false));
            AddColumn("dbo.EquipmentReadings", "Distance2", c => c.Int(nullable: false));
            AddColumn("dbo.EquipmentReadings", "Distance1", c => c.Int(nullable: false));
        }
    }
}
