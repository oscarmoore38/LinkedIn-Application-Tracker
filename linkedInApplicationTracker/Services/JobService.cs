using linkedInApplicationTracker.Data;
using linkedInApplicationTracker.Models; 

namespace linkedInApplicationTracker.Services
{
    public class JobService
    {
        // Insert CRUD operations here. 
        // Tutorial - https://github.com/MicrosoftDocs/mslearn-create-razor-pages-aspnet-core/blob/main/ContosoPizza/Services/PizzaService.cs

        private readonly JobContext _JobContext = default!; 

        public JobService(JobContext JobContext)
        {
            _JobContext = JobContext; 
        }

        // CRUD skeleton code below:
        public IList<Jobs> GetJobs()
        {
            return new List<Jobs>();
        }

        public void AddJob(Jobs job)
        {

        }

        public void DeleteJob(int id)
        {
        
        }



    }
}