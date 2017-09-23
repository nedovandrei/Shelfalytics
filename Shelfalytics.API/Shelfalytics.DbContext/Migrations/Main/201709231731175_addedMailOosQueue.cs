namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedMailOosQueue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailOosQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        ClientId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MailOosQueues");
        }
    }
}
