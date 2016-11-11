using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Bll.Managers;
using WorkingHours.Model.IoC;

namespace WorkingHours.Bll.IoC
{
    public class BllModule : ModelModule
    {
        public override void Load()
        {
            base.Load();
            Bind<IUserManager>().To<UserManager>().InTransientScope();
            Bind<IProjectManager>().To<ProjectManager>().InTransientScope();
            Bind<IIssueManager>().To<IssueManager>().InTransientScope();
            Bind<IWorkTimeManager>().To<WorkTimeManager>().InTransientScope();
        }
    }
}
