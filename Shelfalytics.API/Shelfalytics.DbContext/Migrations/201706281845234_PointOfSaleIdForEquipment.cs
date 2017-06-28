namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointOfSaleIdForEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "PointOfSaleId", c => c.Int(nullable: false));
            AddColumn("dbo.PointOfSales", "PointOfSaleName", c => c.String(nullable: false, maxLength: 200));
            CreateIndex("dbo.Equipments", "PointOfSaleId");
            AddForeignKey("dbo.Equipments", "PointOfSaleId", "dbo.PointOfSales", "Id", cascadeDelete: true);
            DropColumn("dbo.PointOfSales", "CustomerName");
            DropColumn("dbo.PointOfSales", "BankDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PointOfSales", "BankDetails", c => c.String(nullable: false));
            AddColumn("dbo.PointOfSales", "CustomerName", c => c.String(nullable: false, maxLength: 200));
            DropForeignKey("dbo.Equipments", "PointOfSaleId", "dbo.PointOfSales");
            DropIndex("dbo.Equipments", new[] { "PointOfSaleId" });
            DropColumn("dbo.PointOfSales", "PointOfSaleName");
            DropColumn("dbo.Equipments", "PointOfSaleId");
        }
    }
}
