using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace richmedical.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Specialty>().HasMany<Staff>(s => s.Staffs).WithRequired(s => s.Specialty);
        }
    }
}