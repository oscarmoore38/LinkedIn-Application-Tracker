using Microsoft.EntityFrameworkCore;
using linkedInApplicationTracker.Models;

namespace linkedInApplicationTracker.Data
{
    public class ApplicationTrackerContext : DbContext
    {
        public ApplicationTrackerContext (DbContextOptions<ApplicationTrackerContext> options)
            :base(options)
            {
            }
        public DbSet<Application> Applications {get; set;}
        public DbSet<User> Users {get; set;}

        // Explicitly configuring due to requirement for cascade delete
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Applications)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}