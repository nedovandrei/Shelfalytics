namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeShortSKURequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ShortSKUName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ShortSKUName", c => c.String());
        }
    }
}
