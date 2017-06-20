namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 200),
                        BankDetails = c.String(),
                        ContactPersonName = c.String(nullable: false, maxLength: 100),
                        Telephone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeePositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 200),
                        PositionId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        Telephone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeePositions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.EquipmentReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        OpenCloseCount = c.Int(nullable: false),
                        Temperature = c.Int(nullable: false),
                        Distance1 = c.Int(nullable: false),
                        Distance2 = c.Int(nullable: false),
                        Distance3 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .Index(t => t.EquipmentId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        EquipmentTypeId = c.Int(nullable: false),
                        ModelName = c.String(nullable: false, maxLength: 100),
                        Planogram = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EquipmentTypeId);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackagingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PointOfSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 200),
                        Address = c.String(nullable: false, maxLength: 300),
                        BankDetails = c.String(nullable: false),
                        ContactPersonName = c.String(nullable: false, maxLength: 100),
                        Telephone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EAN = c.Int(nullable: false),
                        SKUName = c.String(nullable: false),
                        TradeMark = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Double(nullable: false),
                        UnitOfMeasurement = c.String(nullable: false, maxLength: 20),
                        PackagingTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackagingTypes", t => t.PackagingTypeId, cascadeDelete: true)
                .Index(t => t.PackagingTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "PackagingTypeId", "dbo.PackagingTypes");
            DropForeignKey("dbo.EquipmentReadings", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.EmployeePositions");
            DropForeignKey("dbo.Employees", "ClientId", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "PackagingTypeId" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeId" });
            DropIndex("dbo.Equipments", new[] { "ClientId" });
            DropIndex("dbo.EquipmentReadings", new[] { "EquipmentId" });
            DropIndex("dbo.Employees", new[] { "ClientId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropTable("dbo.Products");
            DropTable("dbo.PointOfSales");
            DropTable("dbo.PackagingTypes");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.EquipmentReadings");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeePositions");
            DropTable("dbo.Clients");
        }
    }
}
