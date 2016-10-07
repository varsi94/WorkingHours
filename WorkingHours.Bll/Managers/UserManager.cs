using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class UserManager : IUserManager
    {
        private IUnitOfWorkFactory UoWFactory { get; }

        public UserManager(IUnitOfWorkFactory uowFactory)
        {
            UoWFactory = uowFactory;
        }

        public void CreateUser(ApplicationUser user, string password)
        {
            using (var uow = UoWFactory.GetUoW())
            {
                uow.Users.Add(user, password);
                uow.SaveChanges();
            }
        }
    }
}
