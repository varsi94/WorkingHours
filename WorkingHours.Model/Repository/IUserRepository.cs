using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(string userName, string password);

        IList<Roles> GetRoles(ApplicationUser user);

        IdentityResult AddToRole(int userId, Roles role);

        IdentityResult Add(ApplicationUser user, string password);

        void ChangePassword(int userId, string oldPassword, string newPassword);
        
        bool IsInRole(int userId, Roles manager);
    }
}
