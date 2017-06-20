namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherChanges : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EmployeePositions", newName: "UserRoles");
            RenameColumn(table: "dbo.Users", name: "PositionId", newName: "RoleId");
            RenameIndex(table: "dbo.Users", name: "IX_PositionId", newName: "IX_RoleId");
            AddColumn("dbo.Clients", "ClientName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.EquipmentReadings", "TimeSpamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.Clients", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "CustomerName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.EquipmentReadings", "TimeSpamp");
            DropColumn("dbo.Clients", "ClientName");
            RenameIndex(table: "dbo.Users", name: "IX_RoleId", newName: "IX_PositionId");
            RenameColumn(table: "dbo.Users", name: "RoleId", newName: "PositionId");
            RenameTable(name: "dbo.UserRoles", newName: "EmployeePositions");
        }
    }
}
