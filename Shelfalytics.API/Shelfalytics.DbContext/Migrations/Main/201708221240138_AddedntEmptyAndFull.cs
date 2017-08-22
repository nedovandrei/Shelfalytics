namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedntEmptyAndFull : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "EmptyDistance", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "FullDistance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "FullDistance");
            DropColumn("dbo.Equipments", "EmptyDistance");
        }
    }
}
