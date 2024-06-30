using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RealtyAgencyMLS.Model;

namespace RealtyAgencyMLS.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().Property(p => p.AppUserID).UseIdentityColumn();
            modelBuilder.Entity<ApplicationUser>().Property(u => u.AppUserID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<ApplicationUser>()
           .HasIndex(u => u.AppUserID)
           .IsUnique();
        }
        #endregion
    }
}
