
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using linkedInApplicationTracker.Models;

namespace linkedInApplicationTracker.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AuthUser class
public class AuthUser : IdentityUser
{
    [Required]
    public string FirstName {get; set;} = default!;
    public string LastName {get; set;} = default!;

    public int UserID {get; set;} // FK to User Profile
    public User User {get; set;} = default!; // Navigation property to User Profile

}

