using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.Exceptions;
using WorkingHours.Web.Models;

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
        [Route("api/account/")]
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
            catch (ConcurrencyException)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/account/whoami")]
        public string GetAccount()
        {
            return User.Identity.Name;
        }
    }
}
