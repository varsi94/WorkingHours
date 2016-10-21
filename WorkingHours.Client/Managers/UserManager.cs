using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Common;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        public UserManager(LoginInfo loginInfo, IAppSettingsManager configurationManager) : base(loginInfo, configurationManager)
        {
        }


    }
}
