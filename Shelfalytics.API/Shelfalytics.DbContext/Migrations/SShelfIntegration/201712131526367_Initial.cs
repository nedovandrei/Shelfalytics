namespace Shelfalytics.DbContext.Migrations.SShelfIntegration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SShelfEquipmentPusherReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentReadingId = c.Int(nullable: false),
                        PusherId = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        Error = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SShelfEquipmentReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModemId = c.String(nullable: false),
                        Signal = c.Int(nullable: false),
                        BatteryLevel = c.Int(nullable: false),
                        GpsLongitude = c.Double(nullable: false),
                        GpsLatitude = c.Double(nullable: false),
                        GsmLongitude = c.Double(nullable: false),
                        GsmLatitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SShelfEquipmentSalesReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentReadingId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SalesCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SShelfEquipmentSalesReadings");
            DropTable("dbo.SShelfEquipmentReadings");
            DropTable("dbo.SShelfEquipmentPusherReadings");
        }
    }
}
