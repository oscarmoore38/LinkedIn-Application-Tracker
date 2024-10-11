using linkedInApplicationTracker.Models;
using linkedInApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace linkedInApplicationTracker.Pages;

    public class UpdateApplication: PageModel
    {
        private readonly ILogger<ManageApplicationModel> _logger;
         [BindProperty]
        public Application? ApplicationToUpdate {get; set;} = default!; 
        private readonly ApplicationTrackerService _applicationTrackerService; 
        public UpdateApplication (ILogger<ManageApplicationModel> logger, ApplicationTrackerService service)

        {
            _applicationTrackerService = service;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                ApplicationToUpdate = await _applicationTrackerService.GetApplicationByIDNoTrackingAsync(id);
            }
             catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding Application with ID: {UserID}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while finding application.");

            }

            if (ApplicationToUpdate == null)
            {
                return NotFound();
            }

            return Page();

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./ManageApplication");
        }


        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            // Fetch ApplicationToUpdate again. This time will be tracked. 
            ApplicationToUpdate = await _applicationTrackerService.GetApplicationByIDAsync(id);

            if (ApplicationToUpdate == null)
            {
                return NotFound(); 
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }

            // Update application 
            if (await TryUpdateModelAsync<Application>(
                ApplicationToUpdate, 
                "ApplicationToUpdate", 
                a => a.UserID,
                a => a.Date, 
                a => a.Title, 
                a => a.Company, 
                a => a.ApplicationURL, 
                a => a.Outcome))
            {
                try
                {
                    await _applicationTrackerService.UpdateApplicationAsync(ApplicationToUpdate); 
                    Console.WriteLine("Success!");
                    return RedirectToPage("./ManageApplication");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating one or more fields for application with ID: {ApplicationID}", ApplicationToUpdate.ApplicationID);

                    ModelState.AddModelError(string.Empty, "An error occurred while updating one or more fields  the application.");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error updating Application.");
            }
            
            Console.WriteLine("Failure");
            return RedirectToPage("./ManageApplication");
            
        }
    }

