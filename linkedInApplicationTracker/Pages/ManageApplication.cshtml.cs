using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;

namespace linkedInApplicationTracker.Pages;

public class ManageApplicationModel : PageModel
{
    private readonly ILogger<ManageApplicationModel> _logger;
    private readonly ApplicationTrackerService _applicationTrackerService = default!;
    [BindProperty]
    public Application Application {get; set;} = default!;
    public User? CurrentUser {get; set;} = default!;
    public int UserID = 1;

    public ManageApplicationModel(ILogger<ManageApplicationModel> logger, ApplicationTrackerService service)
    {
        _logger = logger;
        _applicationTrackerService = service; 
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        if (UserID == null) // Will update once user auth is implemented.
        {
            return NotFound();
        }
        
        // Populate list  
        CurrentUser = await _applicationTrackerService.GetUserByIdAsync(UserID);

        if (CurrentUser == null)
        {
            return NotFound();
        }

        return Page();
 
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid){
            return Page();
        }

        Application.UserID = UserID; // Update once auth is implemented

        if (await TryUpdateModelAsync<Application>(Application, "application", a => a.Date, a => a.Title, a => a.Company, a => a.ApplicationURL, a => a.Outcome))
        {
             await _applicationTrackerService.AddApplicationAsync(Application); 
             return RedirectToPage("./ManageApplication");
        }

        return Page();

    }

}

