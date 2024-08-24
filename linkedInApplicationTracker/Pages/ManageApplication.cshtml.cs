using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linkedInApplicationTracker.Pages;

public class ManageApplicationModel : PageModel
{
    private readonly ILogger<ManageApplicationModel> _logger;
    private readonly ApplicationTrackerService _applicationTrackerService = default!;
    [BindProperty]
    public Application MyApplication {get; set;} = default!;
    public IList<Application> AppList {get; set;} = default!;

    public ManageApplicationModel(ILogger<ManageApplicationModel> logger, ApplicationTrackerService service)
    {
        _logger = logger;
        _applicationTrackerService = service; 
    }
    
    public void OnGet()
    {
        // Populate list 
        AppList = _applicationTrackerService.GetApplications();
    }
    // public IActionResult OnPost()
    // {
    //     if(!ModelState.IsValid)
    // }

}

