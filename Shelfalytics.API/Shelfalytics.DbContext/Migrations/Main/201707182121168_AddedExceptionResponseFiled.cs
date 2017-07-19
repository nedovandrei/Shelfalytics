namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExceptionResponseFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExceptionLogs", "Response", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionLogs", "Response");
        }
    }
}
