using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Managers;
using WorkingHours.Client.Model;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var loginInfo = new LoginInfo();
            var accountManager = new AccountManager(new AppSettingsManager()) { LoginInfo = loginInfo };
            accountManager.LoginAsync("varsi.marci", "123456").Wait();

            var pm = new ProjectManager(new AppSettingsManager())
            {
                LoginInfo = loginInfo
            };

            pm.AddMembersToProjectAsync(2, new Dictionary<int, Roles> {{1, Roles.Employee}})
                .Wait();
        }
    }
}
