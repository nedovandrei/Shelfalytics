namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEquipmentPlanogramTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "RowCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "RowCount");
        }
    }
}
