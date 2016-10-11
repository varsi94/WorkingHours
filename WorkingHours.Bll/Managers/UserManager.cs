using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        public UserManager(IUnitOfWork UoW) : base(UoW)
        {
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                UoW.Users.ChangePassword(userId, oldPassword, newPassword);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("User id not found!");
            }
            catch (ArgumentException)
            {
                throw new UnauthorizedException("Current password is not correct!");
            }
        }

        public void CreateUser(ApplicationUser user, string password)
        {
            var result = UoW.Users.Add(user, password);
            if (!result.Succeeded)
            {
                throw new ArgumentException("Username is already taken!");
            }
            UoW.Users.AddToRole(user.Id, Roles.Employee);
            UoW.SaveChanges();
        }

        public void UpdateRoles(Dictionary<int, Roles> rolesToUpdate)
        {
            foreach (var item in rolesToUpdate)
            {
                var result = UoW.Users.AddToRole(item.Key, item.Value);
                if (!result.Succeeded)
                {
                    throw new InternalServerException($"User with {item.Key} could not be added to role!");
                }
            }
            UoW.SaveChanges();
        }
    }
}
