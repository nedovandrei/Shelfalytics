namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserEquipmentTie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEquipmentTies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentUserId = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserEquipmentTies");
        }
    }
}
