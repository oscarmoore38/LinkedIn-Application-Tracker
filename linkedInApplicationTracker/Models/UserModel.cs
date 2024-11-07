using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using linkedInApplicationTracker.Areas.Identity.Data;


namespace linkedInApplicationTracker.Models
{
    public class User
    {
        public int UserID {get; set;}
        public string UserName { get; set; } = default!;
        [Required]
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public ICollection<Application> Applications {get;set;} = default!;
        public AuthUser AuthUser { get; set; } = default!; // Navigation property

    }
}