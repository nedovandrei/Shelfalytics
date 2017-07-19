namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExceptionTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExceptionLogs", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionLogs", "TimeStamp");
        }
    }
}
