using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace linkedInApplicationTracker.Models
{
    public class Jobs
    {
        public int JobsID {get; set;}
        [Required]
        public string UserID {get; set;} = default!; // FK 
        public DateTime Date {get; set;}
        public string Title {get; set;} = default!;
        public string Company {get; set;} = default!;
        public string JobsURL {get; set;} = default!;
        public string Outcome {get; set;} = default!;

    }
}