using System;
using System.Collections.Generic;
using System.IO;
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

            var projectManager = new ProjectManager(new AppSettingsManager()) {LoginInfo = loginInfo};
            var result = projectManager.GetProjectAsync(1).Result;
            var issue = result.Issues.First();
            issue.Name = "First issue updated";
            issue.Description = "Dummy description";
            issue.Deadline = new DateTime(2017, 1, 1);
            issue.RowVersion = new byte[8];

            var issueManager = new IssueManager(new AppSettingsManager()) {LoginInfo = loginInfo};
            issueManager.UpdateIssueAsync(issue).Wait();
        }
    }
}
