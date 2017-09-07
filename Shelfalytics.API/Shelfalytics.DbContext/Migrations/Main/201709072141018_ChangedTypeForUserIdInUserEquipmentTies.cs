namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypeForUserIdInUserEquipmentTies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEquipmentTies", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.UserEquipmentTies", "AgentUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEquipmentTies", "AgentUserId", c => c.Int(nullable: false));
            DropColumn("dbo.UserEquipmentTies", "UserId");
        }
    }
}
