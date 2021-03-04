namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtherPeopleUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ProductName", c => c.String());
            DropColumn("dbo.Customer", "ModifiedUtc");
        }
    }
}
