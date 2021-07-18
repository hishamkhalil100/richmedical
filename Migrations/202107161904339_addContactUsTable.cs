namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContactUsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactUs");
        }
    }
}
