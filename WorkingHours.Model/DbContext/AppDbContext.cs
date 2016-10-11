using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.DbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        static AppDbContext()
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public UserManager<ApplicationUser, int> UserManager { get; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public AppDbContext() : base("AppContextConnStr")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            UserManager = new UserManager<ApplicationUser, int>(
                new UserStore
                    <ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole,
                        ApplicationUserClaim>(this));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
