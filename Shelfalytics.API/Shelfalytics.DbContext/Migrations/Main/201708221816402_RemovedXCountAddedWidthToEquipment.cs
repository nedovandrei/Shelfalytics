namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedXCountAddedWidthToEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "Width", c => c.Int(nullable: false));
            DropColumn("dbo.Equipments", "XCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "XCount", c => c.Int(nullable: false));
            DropColumn("dbo.Equipments", "Width");
        }
    }
}
