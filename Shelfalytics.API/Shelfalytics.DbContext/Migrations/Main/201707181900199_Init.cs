namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Clients",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ClientName = c.String(nullable: false, maxLength: 100),
            //            Address = c.String(nullable: false, maxLength: 200),
            //            ContactPersonName = c.String(nullable: false, maxLength: 100),
            //            Telephone = c.String(nullable: false, maxLength: 30),
            //            Email = c.String(nullable: false, maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.EquipmentDistanceReadings",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EquipmentReadingId = c.Int(nullable: false),
            //            Distance = c.Int(nullable: false),
            //            Row = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EquipmentReadings", t => t.EquipmentReadingId, cascadeDelete: true)
            //    .Index(t => t.EquipmentReadingId);
            
            //CreateTable(
            //    "dbo.EquipmentReadings",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EquipmentId = c.Int(nullable: false),
            //            TimeSpamp = c.DateTime(nullable: false),
            //            WasOpened = c.Boolean(nullable: false),
            //            Temperature = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
            //    .Index(t => t.EquipmentId);
            
            //CreateTable(
            //    "dbo.Equipments",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            PointOfSaleId = c.Int(nullable: false),
            //            ClientId = c.Int(nullable: false),
            //            EquipmentTypeId = c.Int(nullable: false),
            //            ModelName = c.String(nullable: false, maxLength: 100),
            //            RowCount = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
            //    .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.PointOfSales", t => t.PointOfSaleId, cascadeDelete: true)
            //    .Index(t => t.PointOfSaleId)
            //    .Index(t => t.ClientId)
            //    .Index(t => t.EquipmentTypeId);
            
            //CreateTable(
            //    "dbo.EquipmentTypes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.PointOfSales",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            PointOfSaleName = c.String(nullable: false, maxLength: 200),
            //            Address = c.String(nullable: false, maxLength: 300),
            //            ContactPersonName = c.String(nullable: false, maxLength: 100),
            //            Telephone = c.String(nullable: false),
            //            Email = c.String(nullable: false),
            //            OpeningHour = c.DateTime(nullable: false),
            //            ClosingHour = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.EquipmentPlanograms",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EquipmentId = c.Int(nullable: false),
            //            Row = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
            //    .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.EquipmentId)
            //    .Index(t => t.ProductId);
            
            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EAN = c.String(nullable: false),
            //            SKUName = c.String(nullable: false),
            //            TradeMark = c.String(nullable: false, maxLength: 100),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Volume = c.Double(nullable: false),
            //            UnitOfMeasurement = c.String(nullable: false, maxLength: 20),
            //            PackagingTypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.PackagingTypes", t => t.PackagingTypeId, cascadeDelete: true)
            //    .Index(t => t.PackagingTypeId);
            
            //CreateTable(
            //    "dbo.PackagingTypes",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Exception = c.String(),
                        Request = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserEquipmentTies",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            AgentUserId = c.Int(nullable: false),
            //            EquipmentId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPlanograms", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "PackagingTypeId", "dbo.PackagingTypes");
            DropForeignKey("dbo.EquipmentPlanograms", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentDistanceReadings", "EquipmentReadingId", "dbo.EquipmentReadings");
            DropForeignKey("dbo.EquipmentReadings", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "PointOfSaleId", "dbo.PointOfSales");
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "ClientId", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "PackagingTypeId" });
            DropIndex("dbo.EquipmentPlanograms", new[] { "ProductId" });
            DropIndex("dbo.EquipmentPlanograms", new[] { "EquipmentId" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeId" });
            DropIndex("dbo.Equipments", new[] { "ClientId" });
            DropIndex("dbo.Equipments", new[] { "PointOfSaleId" });
            DropIndex("dbo.EquipmentReadings", new[] { "EquipmentId" });
            DropIndex("dbo.EquipmentDistanceReadings", new[] { "EquipmentReadingId" });
            DropTable("dbo.UserEquipmentTies");
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.PackagingTypes");
            DropTable("dbo.Products");
            DropTable("dbo.EquipmentPlanograms");
            DropTable("dbo.PointOfSales");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.EquipmentReadings");
            DropTable("dbo.EquipmentDistanceReadings");
            DropTable("dbo.Clients");
        }
    }
}
