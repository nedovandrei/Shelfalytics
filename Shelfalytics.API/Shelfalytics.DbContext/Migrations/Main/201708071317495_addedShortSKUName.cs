namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedShortSKUName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortSKUName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShortSKUName");
        }
    }
}
