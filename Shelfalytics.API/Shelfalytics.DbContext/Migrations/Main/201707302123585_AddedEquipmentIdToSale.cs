namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEquipmentIdToSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "EquipmentId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Sales", "EquipmentId");
            //AddForeignKey("dbo.Sales", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "EquipmentId", "dbo.Equipments");
            //DropIndex("dbo.Sales", new[] { "EquipmentId" });
            //DropColumn("dbo.Sales", "EquipmentId");
        }
    }
}
