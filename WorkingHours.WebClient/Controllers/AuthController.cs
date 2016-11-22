using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkingHours.Client.Exceptions;
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
        private readonly IAccountManager accountManager; 

        public AuthController(ILoginManager loginManager, IAccountManager accountManager)
        {
            this.loginManager = loginManager;
            this.accountManager = accountManager;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("MyProjects", "Projects");
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyProjects", "Projects");
            }
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
                ModelState.AddModelError("loginError", "Invalid username or password!");
                return View(new LoginModel());
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

        [HttpGet]
        public ActionResult Signup()
        {
            return View(new SignupModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(SignupModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await accountManager.SignUpAsync(new Shared.Dto.SignUpModel
                {
                    UserName = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    FullName = model.FullName
                });
                return View("SignupSuccessful");
            }
            catch (ServerException e)
            {
                ModelState.AddModelError("SignUpError", e.Message);
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await accountManager.ChangePasswordAsync(new Shared.Dto.PasswordChangeModel
                {
                    OldPassword = model.OldPassword,
                    NewPassword = model.NewPassword
                });
            }
            catch (ServerException)
            {
                ModelState.AddModelError("", "Current password is not valid!");
                return View(model);
            }

            return View("ChangePasswordSuccessful");
        }

        protected override void SetupManagers()
        {
            Managers.Add(accountManager);
        }
    }
}