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
            var accountManager = new AccountManager(new AppSettingsManager()) { LoginInfo = loginInfo };
            accountManager.LoginAsync("varsi.marci", "123456").Wait();

            //var issueManager = new IssueManager(new AppSettingsManager()) {LoginInfo = loginInfo};
            //issueManager.CreateIssueForProject(1, new IssueHeader() {Name ="Issue #2", Deadline = DateTime.UtcNow, Description = "This is created from client."}).Wait();
        }
    }
}
