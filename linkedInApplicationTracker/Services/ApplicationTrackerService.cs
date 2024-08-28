using linkedInApplicationTracker.Data;
using linkedInApplicationTracker.Models;
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
        public async Task<IList<Application>> GetApplicationsAsync()
        {
            if (_ApplicationTrackerContext.Applications != null){
                return await _ApplicationTrackerContext.Applications.Take(10).ToListAsync();
            }
            return new List<Application>();
        }

        public void AddApplication(Application job)
        {

        }

        public void DeleteApplication(int id)
        {
        
        }



    }
}