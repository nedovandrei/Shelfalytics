namespace Shelfalytics.DbContext.Migrations.Main
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhotoPathField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PhotoPath");
        }
    }
}
