using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    internal class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public ApplicationUser Get(string userName, string password)
        {
            return DbContext.UserManager.Find(userName, password);
        }

        public override void Add(ApplicationUser obj)
        {
            throw new InvalidOperationException();
        }

        public override void Remove(ApplicationUser obj)
        {
            DbContext.UserManager.Delete(obj);
        }

        public override void Update(ApplicationUser obj)
        {
            DbContext.UserManager.Update(obj);
        }

        public IdentityResult Add(ApplicationUser user, string password)
        {
            return DbContext.UserManager.Create(user, password);
        }

        public IList<string> GetRoles(ApplicationUser user)
        {
            return DbContext.UserManager.GetRoles(user.Id);
        }

        public IdentityResult AddToRole(int userId, Roles role)
        {
            try
            {
                var currentRoleStr = DbContext.UserManager.GetRoles(userId).FirstOrDefault();
                if (currentRoleStr != null)
                {
                    Roles currentRole;
                    Enum.TryParse(currentRoleStr, out currentRole);
                    if (currentRole == role)
                    {
                        return IdentityResult.Success;
                    }
                    DbContext.UserManager.RemoveFromRole(userId, currentRole.ToString());
                }
                return DbContext.UserManager.AddToRole(userId, role.ToString());
            }
            catch (InvalidOperationException)
            {
                return IdentityResult.Failed("User id not found!");
            }
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var result = DbContext.UserManager.ChangePassword(userId, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new ArgumentException();
            }
        }
    }
}
