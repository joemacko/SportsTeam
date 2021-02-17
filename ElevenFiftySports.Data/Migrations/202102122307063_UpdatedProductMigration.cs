namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "PrimaryId", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "UnitCount", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ProductPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "TypeOfProduct", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Product", "Order_OrderId");
            AddForeignKey("dbo.Product", "Order_OrderId", "dbo.Order", "OrderId");
            DropColumn("dbo.Order", "TotalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "TotalCost", c => c.Double(nullable: false));
            DropForeignKey("dbo.Product", "Order_OrderId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Order_OrderId" });
            DropColumn("dbo.Product", "Order_OrderId");
            DropColumn("dbo.Product", "TypeOfProduct");
            DropColumn("dbo.Product", "ProductPrice");
            DropColumn("dbo.Product", "UnitCount");
            DropColumn("dbo.OrderDetail", "PrimaryId");
        }
    }
}
