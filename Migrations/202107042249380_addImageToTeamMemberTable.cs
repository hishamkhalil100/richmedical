namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageToTeamMemberTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NameAr = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        DescriptionAr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TeamMembers");
        }
    }
}
