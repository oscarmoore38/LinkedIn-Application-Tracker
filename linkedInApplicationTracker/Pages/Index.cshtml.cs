using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using linkedInApplicationTracker.Areas.Identity.Data;
using linkedInApplicationTracker.Services; 
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using linkedInApplicationTracker.Models;


namespace linkedInApplicationTracker.Pages;
[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly ApplicationTrackerService _applicationTrackerService;

    public IndexModel(UserManager<AuthUser> userManager, ApplicationTrackerService applicationTrackerService)
    {
        _userManager = userManager;
        _applicationTrackerService = applicationTrackerService;
    }

    public string WelcomeMessage { get; set; } = string.Empty;
    public int TotalApplications { get; set; } = 0; 
    public int AppliedCount { get; set; } = 0;
    public int InterviewingCount { get; set; } = 0;
    public int RejectedCount { get; set; } = 0;
    public int OfferCount { get; set; } = 0;
    public int AcceptedCount { get; set; } = 0;
    public List<Application> CurrentApplications { get; set;} = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        // Retrieve the current logged-in user
        var authUser = await _userManager.GetUserAsync(User);
        if (authUser == null)
        {
            // Handle the case where the user is not logged in, or redirect to login
            return Challenge();
        }

        WelcomeMessage = $"Welcome {authUser.FirstName}! Below is a summary of your applications.";

        // Fetch the associated user profile
        var userProfile = await _applicationTrackerService.GetUserByIdAsync(authUser.UserID);
        if (userProfile != null)
        {
            CurrentApplications = await _applicationTrackerService.GetAllApplicationsByUserIdAsync(userProfile.UserID);
        }

        if (CurrentApplications.Any())
        {
            TotalApplications = CurrentApplications.Count;
            AppliedCount = CurrentApplications.Count(a => a.Outcome == "Applied");
            InterviewingCount = CurrentApplications.Count(a => a.Outcome == "Interviewing");
            RejectedCount = CurrentApplications.Count(a => a.Outcome == "Rejected");
            OfferCount = CurrentApplications.Count(a => a.Outcome == "Offer");
            AcceptedCount = CurrentApplications.Count(a => a.Outcome == "Accepted");
        }

        return Page();
    }
}

