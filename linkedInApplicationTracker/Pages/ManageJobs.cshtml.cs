using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linkedInApplicationTracker.Pages
{
    public class ManageJobsModel : PageModel
    {
        private readonly ILogger<ManageJobsModel> _logger;
        private readonly JobService _JobService = default!;
        [BindProperty]
        public Jobs Job {get; set;} = default!;
        public IList<Jobs> JobList {get; set;} = default!;

        public ManageJobsModel(ILogger<ManageJobsModel> logger, JobService service)
        {
            _logger = logger;
            _JobService = service; 
        }
        
        public void OnGet()
        {
            // Populate list 
            JobList = _JobService.GetJobs();
        }
        // public IActionResult OnPost()
        // {
        //     if(!ModelState.IsValid)
        // }

    }
}
