namespace Shelfalytics.DbContext.Migrations.SShelfIntegration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeStampForReading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SShelfEquipmentReadings", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SShelfEquipmentReadings", "TimeStamp");
        }
    }
}
