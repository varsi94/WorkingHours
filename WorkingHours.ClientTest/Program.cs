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

            var workTimeManager = new WorkTimeManager(new AppSettingsManager()) {LoginInfo = loginInfo};
            workTimeManager.AddWorkTimeAsync(1, new WorkTimeDto {Name = "", Description = "dummy", Date = DateTime.Now, Hours = 5}).Wait();
        }
    }
}
