namespace richmedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSpecialtiesAndStaffsTabels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        TitleAr = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NameAr = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        DescriptionAr = c.String(),
                        SpecialtyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId, cascadeDelete: true)
                .Index(t => t.SpecialtyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "SpecialtyId", "dbo.Specialties");
            DropIndex("dbo.Staffs", new[] { "SpecialtyId" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Specialties");
        }
    }
}
