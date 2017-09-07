namespace Shelfalytics.DbContext.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypesForIds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GeneralManagerId", x => x.String());
            AlterColumn("dbo.AspNetUsers", "SupervisorId", x => x.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GeneralManagerId", x => x.Int());
            AlterColumn("dbo.AspNetUsers", "SupervisorId", x => x.Int());
        }
    }
}
