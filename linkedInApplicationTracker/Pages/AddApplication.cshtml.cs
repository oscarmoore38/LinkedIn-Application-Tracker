using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;

namespace linkedInApplicationTracker.Pages;

public class AddApplication : PageModel
{
    [BindProperty]
    public Application NewApplication {get; set;} = default!; 
    private readonly ApplicationTrackerService _service; 
    public AddApplication(ApplicationTrackerService service)
    {
        _service = service;
    }
    public void OnGet()
    {
    }
    public IActionResult OnPost()
    {
        if(!ModelState.IsValid || NewApplication == null)
        {
            return Page();
        }

        _service.AddApplication(NewApplication);

        return RedirectToAction("Get");
        
    }
}


