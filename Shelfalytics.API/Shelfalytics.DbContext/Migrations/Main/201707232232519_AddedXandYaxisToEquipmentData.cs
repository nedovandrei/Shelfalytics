namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedXandYaxisToEquipmentData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "XCount", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "YCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "YCount");
            DropColumn("dbo.Equipments", "XCount");
        }
    }
}
