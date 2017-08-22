namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFullAndEmptyDistanceForEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "EmptyDistance", c => c.Double(nullable: false));
            AddColumn("dbo.Equipments", "FullDistance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "FullDistance");
            DropColumn("dbo.Equipments", "EmptyDistance");
        }
    }
}
