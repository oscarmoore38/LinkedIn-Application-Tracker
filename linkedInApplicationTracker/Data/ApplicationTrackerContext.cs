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

            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)               
                .WithMany(u => u.Applications)     
                .HasForeignKey(a => a.UserID)      
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}