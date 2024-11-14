using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using linkedInApplicationTracker.Areas.Identity.Data;


namespace linkedInApplicationTracker.Models
{
    public class User
    {
        public int UserID {get; set;}

        public ICollection<Application> Applications {get;set;} = default!;
    }
}