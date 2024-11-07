using linkedInApplicationTracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace linkedInApplicationTracker.Areas.Identity.Data;

public class linkedInApplicationTrackerIdentityDbContext : IdentityDbContext<AuthUser>
{
    public linkedInApplicationTrackerIdentityDbContext(DbContextOptions<linkedInApplicationTrackerIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Renaming tables to avoid conflict with existing "User" table
        // Renaming Identity tables
        builder.Entity<AuthUser>(b => {
            b.ToTable("Account");
        });
        
        builder.Entity<IdentityRole>(b => {
            b.ToTable("Roles");
        });
        
        builder.Entity<IdentityUserRole<string>>(b => {
            b.ToTable("URoles");
        });
        
        builder.Entity<IdentityUserClaim<string>>(b => {
            b.ToTable("UClaims");
        });
        
        builder.Entity<IdentityUserLogin<string>>(b => {
            b.ToTable("Logins");
        });
        
        builder.Entity<IdentityRoleClaim<string>>(b => {
            b.ToTable("RoleClaims");
        });
        
        builder.Entity<IdentityUserToken<string>>(b => {
            b.ToTable("UTokens");
        });
    }
}
