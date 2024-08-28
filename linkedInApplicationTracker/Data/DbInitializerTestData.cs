using linkedInApplicationTracker.Models;

namespace linkedInApplicationTracker.Data

{
    public static class DbInitializer {
        
        public static void Initialize (ApplicationTrackerContext context) 
        {   
            // Ignore if already seeded
            if (context.Users.Any()){
                return; 
            }

            // Seed with test data
            var users = new User[]
            {
                new User{UserName="jdoe", FirstName="John", LastName="Doe"},
                new User{UserName="asmith", FirstName="Alice", LastName="Smith"},
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var applications = new Application[]
            {
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-01"), Title = "Software Engineer", Company = "TechCorp", ApplicationURL = "https://techcorp.com/jobs/software-engineer", Outcome = "Interviewed" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-03"), Title = "Data Analyst", Company = "DataPros", ApplicationURL = "https://datapros.com/jobs/data-analyst", Outcome = "Rejected" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-05"), Title = "Frontend Developer", Company = "WebWorks", ApplicationURL = "https://webworks.com/careers/frontend-developer", Outcome = "Applied" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-07"), Title = "UX Designer", Company = "DesignIt", ApplicationURL = "https://designit.com/jobs/ux-designer", Outcome = "Offered" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-09"), Title = "DevOps Engineer", Company = "CloudMasters", ApplicationURL = "https://cloudmasters.com/jobs/devops-engineer", Outcome = "Interviewed" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-11"), Title = "Database Administrator", Company = "DataGuard", ApplicationURL = "https://dataguard.com/careers/database-administrator", Outcome = "Applied" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-13"), Title = "Project Manager", Company = "ManagePlus", ApplicationURL = "https://manageplus.com/jobs/project-manager", Outcome = "Rejected" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-15"), Title = "Backend Developer", Company = "CodeFactory", ApplicationURL = "https://codefactory.com/careers/backend-developer", Outcome = "Offered" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-17"), Title = "Systems Analyst", Company = "SysAnalyze", ApplicationURL = "https://sysanalyze.com/jobs/systems-analyst", Outcome = "Interviewed" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-19"), Title = "Security Specialist", Company = "SecureNet", ApplicationURL = "https://securenet.com/jobs/security-specialist", Outcome = "Applied" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-21"), Title = "Marketing Manager", Company = "MarketPros", ApplicationURL = "https://marketpros.com/careers/marketing-manager", Outcome = "Rejected" },
                new Application { UserID = 1, Date = DateTime.Parse("2024-05-23"), Title = "Data Scientist", Company = "DataInsights", ApplicationURL = "https://datainsights.com/jobs/data-scientist", Outcome = "Offered" },
            };

            context.Applications.AddRange(applications);
            context.SaveChanges();


        }
    }
    
}