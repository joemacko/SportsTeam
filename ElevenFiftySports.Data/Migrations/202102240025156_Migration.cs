namespace ElevenFiftySports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "ModifiedUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
    }
}
