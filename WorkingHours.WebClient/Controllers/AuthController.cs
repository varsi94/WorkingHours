using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkingHours.Client.Interfaces;
using WorkingHours.WebClient.Common;
using WorkingHours.WebClient.Extensions;
using WorkingHours.WebClient.Interfaces;
using WorkingHours.WebClient.Models;

namespace WorkingHours.WebClient.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ILoginManager loginManager;

        public AuthController(ILoginManager loginManager)
        {
            this.loginManager = loginManager;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("login", "auth");
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginModel() {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var result = await loginManager.LoginAsync(model);
            if (result != null)
            {
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(result);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            else
            {
                return RedirectToAction("MyProjects", "Projects");
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("login", "auth");
        }

        [Authorize]
        public string Dummy()
        {
            return User.GetUsername();
        }

        protected override void SetupManagers()
        {
        }
    }
}