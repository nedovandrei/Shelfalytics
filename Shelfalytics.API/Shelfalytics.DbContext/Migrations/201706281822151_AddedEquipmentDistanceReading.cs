namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEquipmentDistanceReading : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentDistanceReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        EquipmentReadingId = c.Int(nullable: false),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentReadings", t => t.EquipmentReadingId, cascadeDelete: true)
                .Index(t => t.EquipmentReadingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentDistanceReadings", "EquipmentReadingId", "dbo.EquipmentReadings");
            DropIndex("dbo.EquipmentDistanceReadings", new[] { "EquipmentReadingId" });
            DropTable("dbo.EquipmentDistanceReadings");
        }
    }
}
