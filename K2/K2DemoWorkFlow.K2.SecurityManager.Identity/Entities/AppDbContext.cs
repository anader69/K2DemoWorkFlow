using System.Configuration;
using System.Data.Entity;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        //: base(Environment.GetEnvironmentVariable("K2IdentityConnectionString", EnvironmentVariableTarget.Machine) == "" ? "":"")
        : base(ConfigurationManager.AppSettings["DSCK2Provider"])
        {
        }

        public virtual DbSet<ApplicationRole> AspNetRoles { get; set; }
        public virtual DbSet<UserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<UserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
