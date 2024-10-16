using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.SignalR;

namespace linkedInApplicationTracker.Models
{
    public class Application
    {
        public int ApplicationID {get; set;}
        [Required]
        // Foregin Key
        public int UserID {get; set;} = default!; 
        [ValidateNever]
        // Navigation property to the user
        public User User { get; set; } = default!;
        public DateTime Date {get; set;}
        public string Title {get; set;} = default!;
        public string Company {get; set;} = default!;
        public string ApplicationURL {get; set;} = default!;
        public string Outcome {get; set;} = default!;

    }
}