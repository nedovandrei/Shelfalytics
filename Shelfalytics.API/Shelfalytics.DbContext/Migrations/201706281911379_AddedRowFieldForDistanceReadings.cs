namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRowFieldForDistanceReadings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentDistanceReadings", "Row", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentDistanceReadings", "Row");
        }
    }
}
