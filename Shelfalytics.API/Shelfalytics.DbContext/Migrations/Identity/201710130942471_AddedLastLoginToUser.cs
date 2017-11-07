namespace Shelfalytics.DbContext.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLastLoginToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLogin");
        }
    }
}
