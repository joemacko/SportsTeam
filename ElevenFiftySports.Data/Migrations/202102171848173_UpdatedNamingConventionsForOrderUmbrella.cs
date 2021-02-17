namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNamingConventionsForOrderUmbrella : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderDetail", newName: "OrderProduct");
            CreateIndex("dbo.Order", "CustomerId");
            AddForeignKey("dbo.Order", "CustomerId", "dbo.Customer", "CustomerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerId" });
            RenameTable(name: "dbo.OrderProduct", newName: "OrderDetail");
        }
    }
}
