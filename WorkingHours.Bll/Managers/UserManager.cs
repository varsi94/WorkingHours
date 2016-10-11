using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class UserManager : IUserManager
    {
        private IUnitOfWork UoW { get; }

        public UserManager(IUnitOfWork uow)
        {
            UoW = uow;
        }

        public void CreateUser(ApplicationUser user, string password)
        {
            var result = UoW.Users.Add(user, password);
            if (!result.Succeeded)
            {
                throw new ArgumentException("Username is already taken!");
            }
            UoW.Users.AddToRole(user, Roles.Employee);
            UoW.SaveChanges();
        }
    }
}
