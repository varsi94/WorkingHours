using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkingHours.WebClient.Common;

namespace WorkingHours.WebClient.Controllers
{
    public class ErrorsController : BaseController
    {
        public ActionResult Error404()
        {
            return View();
        }

        protected override void SetupManagers()
        {
        }
    }
}