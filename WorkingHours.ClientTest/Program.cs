using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Managers;
using WorkingHours.Client.Model;
using WorkingHours.Shared.Dto;

namespace WorkingHours.ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var loginInfo = new LoginInfo();
            var accountManager = new AccountManager(loginInfo, new AppSettingsManager());
            accountManager.LoginAsync("varsi.marci", "123456").Wait();

            var projectManager = new ProjectManager(loginInfo, new AppSettingsManager());
            var result = projectManager.GetProjectAsync(1).Result;
        }
    }
}
