using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Managers;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.IoC
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountManager>().To<AccountManager>().InTransientScope();
            Bind<LoginInfo>().ToSelf().InSingletonScope();
            Bind<IAppSettingsManager>().To<AppSettingsManager>().InSingletonScope();
            Bind<IProjectManager>().To<ProjectManager>().InTransientScope();
            Bind<IUserManager>().To<UserManager>().InTransientScope();
            Bind<IIssueManager>().To<IssueManager>().InTransientScope();
            Bind<IWorkTimeManager>().To<WorkTimeManager>().InTransientScope();
        }
    }
}
