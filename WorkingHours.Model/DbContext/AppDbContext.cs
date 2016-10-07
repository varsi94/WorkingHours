using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.DbContext
{
    internal class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public UserManager<ApplicationUser, int> UserManager { get; }

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
