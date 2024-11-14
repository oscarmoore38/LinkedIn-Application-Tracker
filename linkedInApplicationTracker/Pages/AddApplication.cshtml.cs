using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using linkedInApplicationTracker.Areas.Identity.Data;


namespace linkedInApplicationTracker.Pages;

public class AddApplication : PageModel
{
    private readonly ILogger<AddApplication> _logger;
    private readonly UserManager<AuthUser> _userManager;
    private readonly ApplicationTrackerService  _applicationTrackerService; 

    [BindProperty]
    public Application NewApplication {get; set;} = default!; 
    public User? CurrentUserApplicationsProfile {get; set;} = default!; 
    
    
    public AddApplication(UserManager<AuthUser> userManager, ApplicationTrackerService ApplicationTrackerService, ILogger<AddApplication> logger)
    {
        _logger = logger;
        _applicationTrackerService = ApplicationTrackerService;
        _userManager = userManager;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        // Get Current Logged in User 
        var CurrentLoggedInUser = await _userManager.GetUserAsync(User);

        // Redirect to login if null
        if (CurrentLoggedInUser == null) 
        {
            return Challenge();
        }

        // Load logged in users applications profile
        CurrentUserApplicationsProfile = await _applicationTrackerService.GetUserByIdAsync(CurrentLoggedInUser.UserID);

        if (CurrentUserApplicationsProfile == null)
        {
            return NotFound();
        }

        return Page();
    }
    public async Task<IActionResult> OnPostAsync(int id)
    {
        if(!ModelState.IsValid || NewApplication == null)
        {
            return Page();
        }

        Console.WriteLine($"Here is the current ID: {id}");

        try
        {
            await _applicationTrackerService.AddApplicationAsync(NewApplication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding the following application: {NewApplication}", NewApplication);

            ModelState.AddModelError(string.Empty, "An error occurred while adding application.");

        }
        return RedirectToPage("./ManageApplication");
        
    }
}


