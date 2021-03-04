namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JoeMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ProductName", c => c.String());
        }
    }
}
