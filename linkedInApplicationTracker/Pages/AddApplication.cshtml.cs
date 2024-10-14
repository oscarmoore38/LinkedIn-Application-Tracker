using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.Identity.Client;

namespace linkedInApplicationTracker.Pages;

public class AddApplication : PageModel
{
    private readonly ILogger<AddApplication> _logger;
    [BindProperty]
    public Application NewApplication {get; set;} = default!; 
    private readonly ApplicationTrackerService _service; 
    public User? CurrentUser {get; set;} = default!;
    public int UserID = 1; // Will update once user auth is implemented.
    
    
    public AddApplication(ApplicationTrackerService service, ILogger<AddApplication> logger)
    {
        _logger = logger;
        _service = service;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        // Check current user - will update and change code once auth is implemented
        if (UserID == null)
        {
            return NotFound();
        }
        
        try 
        {
            CurrentUser = await _service.GetUserByIdAsync(UserID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while finding user with ID: {UserID}", UserID);

            ModelState.AddModelError(string.Empty, "An error occurred while finding user.");
        }

        if (CurrentUser == null)
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
            await _service.AddApplicationAsync(NewApplication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding the following application: {NewApplication}", NewApplication);

            ModelState.AddModelError(string.Empty, "An error occurred while adding application.");

        }
        return RedirectToPage("./ManageApplication");
        
    }
}


