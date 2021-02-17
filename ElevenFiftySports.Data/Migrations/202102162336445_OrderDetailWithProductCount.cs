namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDetailWithProductCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "ProductCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetail", "ProductCount");
        }
    }
}
