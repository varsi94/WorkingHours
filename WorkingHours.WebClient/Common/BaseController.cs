using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WorkingHours.Client.Common;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.WebClient.Extensions;

namespace WorkingHours.WebClient.Common
{
    public abstract class BaseController : Controller
    {
        protected List<IManager> Managers { get; } = new List<IManager>();

        protected abstract void SetupManagers();

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
            SetupManagers();
            var loginInfo = Mapper.Map<LoginInfo>(User);

            foreach (var manager in Managers)
            {
                manager.LoginInfo = loginInfo;
            }
        }
    }
}