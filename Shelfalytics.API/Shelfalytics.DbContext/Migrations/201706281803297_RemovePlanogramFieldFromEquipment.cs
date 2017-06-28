namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePlanogramFieldFromEquipment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Equipments", "Planogram");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "Planogram", c => c.String());
        }
    }
}
