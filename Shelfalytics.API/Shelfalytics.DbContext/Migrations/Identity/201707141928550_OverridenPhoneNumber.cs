namespace Shelfalytics.DbContext.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverridenPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "Telephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Telephone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
        }
    }
}
