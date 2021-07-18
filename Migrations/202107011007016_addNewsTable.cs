namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        TitleAr = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
