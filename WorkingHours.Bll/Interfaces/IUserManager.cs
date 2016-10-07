using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Bll.Interfaces
{
    public interface IUserManager
    {
        void CreateUser(ApplicationUser user, string password);
    }
}
