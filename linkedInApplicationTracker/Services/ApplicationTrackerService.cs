using linkedInApplicationTracker.Data;
using linkedInApplicationTracker.Models; 

namespace linkedInApplicationTracker.Services
{
    public class ApplicationTrackerService
    {
        // Insert CRUD operations here. 
        // Tutorial - https://github.com/MicrosoftDocs/mslearn-create-razor-pages-aspnet-core/blob/main/ContosoPizza/Services/PizzaService.cs

        private readonly ApplicationTrackerContext _ApplicationTrackerContext = default!; 

        public ApplicationTrackerService(ApplicationTrackerContext ApplicationTrackerContext)
        {
            _ApplicationTrackerContext = ApplicationTrackerContext; 
        }

        // CRUD skeleton code below:
        public IList<Application> GetApplications()
        {
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