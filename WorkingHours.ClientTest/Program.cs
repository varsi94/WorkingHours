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
            var result = workTimeManager.GetWorkTimesForManagerAsync(1, 10, 1).Result;

            var ecsedi = result.Items.First(x => x.Employee.Username == "varsi.marci");
            workTimeManager.UpdateWorkTimeAsync(new UpdateWorkTimeDto
            {
                Name = "first worktime",
                Description = "asd",
                Date = DateTime.Now,
                Hours = 2.5,
                Id = 50,
                RowVersion = ecsedi.RowVersion
            }).Wait();
        }
    }
}
