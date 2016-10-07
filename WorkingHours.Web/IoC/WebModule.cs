using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Bll.IoC;
using WorkingHours.Web.Controllers;

namespace WorkingHours.Web.IoC
{
    public class WebModule : BllModule
    {
        public override void Load()
        {
            base.Load();
            Bind<AccountController>().To<AccountController>().InTransientScope();
        }
    }
}