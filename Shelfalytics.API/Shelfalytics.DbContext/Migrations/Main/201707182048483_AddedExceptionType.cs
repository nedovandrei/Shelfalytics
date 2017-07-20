namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExceptionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExceptionLogs", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionLogs", "Type");
        }
    }
}
