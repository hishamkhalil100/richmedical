namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreationdateToNewsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "CreationDate");
        }
    }
}
