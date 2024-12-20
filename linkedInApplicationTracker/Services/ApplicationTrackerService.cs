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

        public async Task AddUserAsync(User newUser)
        {
            await _ApplicationTrackerContext.Users.AddAsync(newUser);
            await _ApplicationTrackerContext.SaveChangesAsync();
        
        }


        public async Task<User?> GetUserByIdAsync(int userID)
        {
            
            return await _ApplicationTrackerContext.Users
                .FirstOrDefaultAsync(u => u.UserID == userID);

        }

        public IQueryable<Application> GetApplicationsByUserId(int userID, string searchString)
        {
            // Get applications for user 
            var query = _ApplicationTrackerContext.Applications
                                                .Where(a => a.UserID == userID);

            // Apply additional filters if the search string is not null or empty
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                // Case insensitive search 
                query = query.Where(a => 
                    a.Company.ToLower().Contains(searchString) ||
                    a.Title.ToLower().Contains(searchString) ||
                    a.Outcome.ToLower().Contains(searchString));
            }
            return query;
            }

        public async Task<PaginatedList<Application>> GetPagedApplicationsAsync(IQueryable<Application> applicationIQ, int pageIndex, int pageSize)
        {
            return await PaginatedList<Application>.CreateAsync(applicationIQ.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<List<Application>> GetAllApplicationsByUserIdAsync(int userId)
        {
            return await _ApplicationTrackerContext.Users
                .Where(u => u.UserID == userId)
                .Include(u => u.Applications) 
                .SelectMany(u => u.Applications) 
                .ToListAsync();
        }

        public void UpdateUser(int id)
        {
        
        }

        public void DeleteUser(int id)
        {
        
        }

        public async Task<Application?> GetApplicationByIDAsync(int applicationID)
        {
            return await _ApplicationTrackerContext.Applications
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationID == applicationID);
        }

        // Use for read operations for improved preformance. 
        public async Task<Application?> GetApplicationByIDNoTrackingAsync(int applicationID)
        {
            return await _ApplicationTrackerContext.Applications
                .AsNoTracking()
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationID == applicationID);
        }

        public async Task AddApplicationAsync(Application applicationToAdd)
        {
            await _ApplicationTrackerContext.Applications.AddAsync(applicationToAdd);
            await _ApplicationTrackerContext.SaveChangesAsync();
        }


        public async Task UpdateApplicationAsync(Application applicationToUpdate)
        {
            await _ApplicationTrackerContext.SaveChangesAsync();
        }

        public async Task DeleteApplicationAsync(Application applicationToDelete)
        {
            _ApplicationTrackerContext.Applications.Remove(applicationToDelete);
            await _ApplicationTrackerContext.SaveChangesAsync();
        }

    }
}