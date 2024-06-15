using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linkedInApplicationTracker.Pages
{
    public class ManageJobsModel : PageModel
    {
        private readonly ILogger<ManageJobsModel> _logger;

        public ManageJobsModel(ILogger<ManageJobsModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
