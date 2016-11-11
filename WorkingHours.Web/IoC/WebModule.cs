using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Bll.IoC;
using WorkingHours.Model.DbContext;
using WorkingHours.Web.App_Start;
using WorkingHours.Web.Controllers;

namespace WorkingHours.Web.IoC
{
    public class WebModule : BllModule
    {
        public override void Load()
        {
            base.Load();
            Bind<AccountController>().ToSelf().InRequestScope();
            Bind<ProjectController>().ToSelf().InRequestScope();
            Bind<IssueController>().ToSelf().InRequestScope();
            Bind<WorkTimeController>().ToSelf().InRequestScope();
            Bind<AppDbContext>().ToSelf().InRequestScope();
        }
    }
}