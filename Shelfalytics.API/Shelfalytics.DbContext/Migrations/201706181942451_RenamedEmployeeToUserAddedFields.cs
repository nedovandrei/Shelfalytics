namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedEmployeeToUserAddedFields : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Employees", newName: "Users");
            AddColumn("dbo.Users", "GeneralManagerId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "SupervisorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SupervisorId");
            DropColumn("dbo.Users", "GeneralManagerId");
            RenameTable(name: "dbo.Users", newName: "Employees");
        }
    }
}
