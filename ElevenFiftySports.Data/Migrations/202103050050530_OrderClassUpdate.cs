namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderClassUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "FinalTotalCost", c => c.Double(nullable: false));
            AddColumn("dbo.Order", "OrderFinalized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderFinalized");
            DropColumn("dbo.Order", "FinalTotalCost");
        }
    }
}
