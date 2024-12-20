using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using linkedInApplicationTracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace linkedInApplicationTracker.Pages;

public class ManageApplicationModel : PageModel
{
    private readonly ILogger<ManageApplicationModel> _logger;
    private readonly ApplicationTrackerService _applicationTrackerService = default!;
    private readonly UserManager<AuthUser> _userManager = default!; 
    private readonly IConfiguration Configuration; 
    [BindProperty]
    public Application Application {get; set;} = default!;
    public string? DateSort { get; set; }
    public string? CompanySort { get; set; }
    public string? TitleSort { get; set; }
    public string? OutcomeSort { get; set; }
    public string? CurrentFilter { get; set; }
    public string? CurrentSort { get; set; }
    public PaginatedList<Application>? CurrentUsersApplications {get; set;}

    public ManageApplicationModel(ILogger<ManageApplicationModel> logger, UserManager<AuthUser> userManager, ApplicationTrackerService service, IConfiguration configuration)
    {
        _logger = logger;
        _applicationTrackerService = service; 
        _userManager = userManager;
        Configuration = configuration;

    }
    
    public async Task<IActionResult> OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
    {
        CurrentSort = sortOrder; 
        DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : ""; // date ascending by default.
        CompanySort = sortOrder == "Company" ? "company_desc" : "Company";
        OutcomeSort = sortOrder == "Outcome" ? "outcome_desc" : "Outcome";
        TitleSort = sortOrder == "Title" ? "title_desc" : "Title";
        if (searchString != null)
        {
            pageIndex = 1; 
        }
        else
        {
            searchString = currentFilter;
        }
        CurrentFilter = searchString;

        // Get Current Logged in User 
        var CurrentLoggedInUser = await _userManager.GetUserAsync(User);

        // Redirect to login if null
        if (CurrentLoggedInUser == null) 
        {
            return Challenge();
        }

        // Load logged in users applications profile
        var CurrentUserApplicationsProfile = await _applicationTrackerService.GetUserByIdAsync(CurrentLoggedInUser.UserID);

        if (CurrentUserApplicationsProfile == null)
        {
            return NotFound();
        }

        // Load Applications for current user 
        IQueryable<Application> applicationIQ =_applicationTrackerService.GetApplicationsByUserId(CurrentLoggedInUser.UserID, searchString);    
        
        // Apply sorting
        switch (sortOrder)
        {
            case "date_desc":
                applicationIQ = applicationIQ.OrderByDescending(a => a.Date);
                break;
            case "Company":
                applicationIQ = applicationIQ.OrderBy(a => a.Company);
                break;
            case "company_desc":
                applicationIQ = applicationIQ.OrderByDescending(a => a.Company);
                break;
            case "Outcome":
                applicationIQ = applicationIQ.OrderBy(a => a.Outcome);
                break;
            case "outcome_desc":
                applicationIQ = applicationIQ.OrderByDescending(a => a.Outcome);
                break;
            case "Title":
                applicationIQ = applicationIQ.OrderBy(a => a.Title);
                break;
            case "title_desc":
                applicationIQ = applicationIQ.OrderByDescending(a => a.Title);
                break;
            default:
                applicationIQ = applicationIQ.OrderBy(a => a.Date);
                break;
        }

        var pageSize = Configuration.GetValue("PageSize", 10);
        // Execute Query
        try
        {
             CurrentUsersApplications = await _applicationTrackerService.GetPagedApplicationsAsync(applicationIQ, pageIndex ?? 1, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting applications for user with ID: {UserID}", CurrentLoggedInUser.UserID);

            ModelState.AddModelError(string.Empty, "An error occurred while getting user applications.");

        }

        return Page();
 
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // Check application exists.  
        var applicationToDelete = await _applicationTrackerService.GetApplicationByIDAsync(id);

        if (applicationToDelete != null)
        {
            try{
                await _applicationTrackerService.DeleteApplicationAsync(applicationToDelete);
                return RedirectToPage("./ManageApplication");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting application with ID: {ApplicationID}", applicationToDelete.ApplicationID);

                ModelState.AddModelError(string.Empty, "An error occurred while deleting the application.");
            }

        }
        else
        {
            ModelState.AddModelError(string.Empty, "Application not found.");
        }

        return RedirectToPage();

    }
    
    public IActionResult OnPostEdit(int id)
    {

        return RedirectToPage("./UpdateApplication", new { id = id });
    
    }

}

