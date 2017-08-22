namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedEmptyAndFull : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Equipments", "EmptyDistance");
            DropColumn("dbo.Equipments", "FullDistance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "FullDistance", c => c.Double(nullable: false));
            AddColumn("dbo.Equipments", "EmptyDistance", c => c.Double(nullable: false));
        }
    }
}
