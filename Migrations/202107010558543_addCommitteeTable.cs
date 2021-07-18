namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommitteeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        NameAr = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Committees");
        }
    }
}
