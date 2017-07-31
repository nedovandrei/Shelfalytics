namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEquipmentIdToSale2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Sales", "EquipmentId");
            AddForeignKey("dbo.Sales", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sales", new[] { "EquipmentId" });
            DropColumn("dbo.Sales", "EquipmentId");
        }
    }
}
