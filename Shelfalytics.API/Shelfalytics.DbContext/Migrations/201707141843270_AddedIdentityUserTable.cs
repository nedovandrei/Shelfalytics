namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdentityUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "ClientId" });
            //DropTable("dbo.UserRoles");
            //DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 200),
                        RoleId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        Telephone = c.String(nullable: false),
                        GeneralManagerId = c.Int(nullable: false),
                        SupervisorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Users", "ClientId");
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.UserRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
