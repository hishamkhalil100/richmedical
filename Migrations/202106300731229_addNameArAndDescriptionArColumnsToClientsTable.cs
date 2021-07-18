namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNameArAndDescriptionArColumnsToClientsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NameAr", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "DescriptionAr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "DescriptionAr");
            DropColumn("dbo.Clients", "NameAr");
        }
    }
}
