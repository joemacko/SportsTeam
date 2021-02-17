namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDetailMigration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderId" });
            DropColumn("dbo.Product", "Order_OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Product", "Order_OrderId");
            AddForeignKey("dbo.Product", "Order_OrderId", "dbo.Order", "OrderId");
        }
    }
}
