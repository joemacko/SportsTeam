namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Special", "DayOfWeeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Special", "DayOfWeeek", c => c.DateTime(nullable: false));
        }
    }
}
