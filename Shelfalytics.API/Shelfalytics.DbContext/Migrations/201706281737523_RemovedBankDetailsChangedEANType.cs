namespace Shelfalytics.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBankDetailsChangedEANType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "EAN", c => c.String(nullable: false));
            DropColumn("dbo.Clients", "BankDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "BankDetails", c => c.String());
            AlterColumn("dbo.Products", "EAN", c => c.Int(nullable: false));
        }
    }
}
