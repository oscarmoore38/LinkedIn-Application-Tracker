using linkedInApplicationTracker.Data;
using linkedInApplicationTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace linkedInApplicationTracker.Services
{
    public class ApplicationTrackerService
    {
        private readonly ApplicationTrackerContext _ApplicationTrackerContext = default!; 

        public ApplicationTrackerService(ApplicationTrackerContext ApplicationTrackerContext)
        {
            _ApplicationTrackerContext = ApplicationTrackerContext; 
        }

        // CRUD skeleton code below:

        public void AddUser()
        {
        
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            
            return await _ApplicationTrackerContext.Users
                .Include(u => u.Applications)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserID == id);

        }

        public void UpdateUser(int id)
        {
        
        }

        public void DeleteUser(int id)
        {
        
        }

        public async Task AddApplicationAsync(Application Application)
        {
            _ApplicationTrackerContext.Add(Application);
            await _ApplicationTrackerContext.SaveChangesAsync();
        }


        public void UpdateApplication(Application job)
        {

        }

        public void DeleteApplication(int id)
        {
        
        }

    }
}