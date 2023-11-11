using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection.Emit;
using Ticketing.Models;

namespace Ticketing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tickets> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<Tickets>().HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Tickets>().HasOne(t => t.Technician).WithMany().HasForeignKey(t => t.TechnicianId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Tickets>().HasOne(t => t.LastUpdatedBy).WithMany().HasForeignKey(t => t.LastUpdatedById).OnDelete(DeleteBehavior.Restrict);


            //Seeding
            builder.Entity<Tickets>().HasData(new Tickets { Id = 1, Status = "New", Category = "It", Severity = "high" });
        }
    }
}