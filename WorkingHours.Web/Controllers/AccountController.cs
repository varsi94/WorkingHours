using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.Exceptions;
using WorkingHours.Model.UoW;
using WorkingHours.Web.Extensions;
using WorkingHours.Web.Models;

namespace WorkingHours.Web.Controllers
{
    public class AccountController : ApiController
    {
        private IUserManager UserManager { get; }

        private Guid Guid { get; }

        public AccountController(IUserManager userManager, IUnitOfWork uow)
        {
            UserManager = userManager;
            Guid = Guid.NewGuid();
            Debug.WriteLine("AccountController created: " + Guid.ToString());
        }

        [HttpPost]
        [Route("api/account/signup")]
        public IHttpActionResult CreateAccount([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
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
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("api/account/whoami")]
        public IHttpActionResult GetAccount()
        {
            var result = new UserData
            {
                Email = User.Identity.GetEmail(),
                FullName = User.Identity.GetFullName(),
                UserName = User.Identity.Name,
                Roles = User.Identity.GetRoles()
            };
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Debug.WriteLine("AccountController disposed: " + Guid.ToString());
        }
    }
}
