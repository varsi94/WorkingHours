using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.Exceptions;
using WorkingHours.Model.UoW;
using WorkingHours.Shared.Dto;
using WorkingHours.Web.Extensions;

namespace WorkingHours.Web.Controllers
{
    public class AccountController : ApiController
    {
        private IUserManager UserManager { get; }

        public AccountController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        [HttpPost]
        [Route("api/account/signup")]
        public IHttpActionResult CreateAccount([FromBody] SignUpModel user)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var appUser = new ApplicationUser();
                UserManager.CreateUser(new ApplicationUser
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email
                }, user.Password);
                return Ok();
            }
            catch (ArgumentException)
            {
                return this.Conflict();
            }
        }

        [Authorize]
        [Route("api/account/whoami")]
        [HttpGet]
        public IHttpActionResult GetAccount()
        {
            var result = new UserHeaderDto
            {
                Email = User.Identity.GetEmail(),
                FullName = User.Identity.GetFullName(),
                Username = User.Identity.Name,
                Role = User.Identity.GetRoles().Single()
            };
            return Ok(result);
        }

        [Authorize]
        [Route("api/account/changePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword([FromBody] PasswordChangeModel pwdChange)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            UserManager.ChangePassword(User.Identity.GetUserId(), pwdChange.OldPassword, pwdChange.NewPassword);
            return Ok();
        }
    }
}
