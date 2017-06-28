namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EquipmentPlanograms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentPlanograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.EquipmentId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPlanograms", "ProductId", "dbo.Products");
            DropForeignKey("dbo.EquipmentPlanograms", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.EquipmentPlanograms", new[] { "ProductId" });
            DropIndex("dbo.EquipmentPlanograms", new[] { "EquipmentId" });
            DropTable("dbo.EquipmentPlanograms");
        }
    }
}
