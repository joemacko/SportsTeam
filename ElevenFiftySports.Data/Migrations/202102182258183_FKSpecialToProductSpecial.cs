namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKSpecialToProductSpecial : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Special", "ProductId");
            AddForeignKey("dbo.Special", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Special", "ProductId", "dbo.Product");
            DropIndex("dbo.Special", new[] { "ProductId" });
        }
    }
}
