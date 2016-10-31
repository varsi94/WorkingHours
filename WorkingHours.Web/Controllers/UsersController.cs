using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Web.Extensions;

namespace WorkingHours.Web.Controllers
{
    public class UsersController : ApiController
    {
        private IUserManager UserManager { get; }

        public UsersController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        [Route("api/users/")]
        [HttpGet]
        [AuthorizeRoles(Roles.Manager)]
        public IHttpActionResult ListUsers([FromUri] int? pageSize = null, [FromUri]int? pageIndex = null, [FromUri]string name = null)
        {
            return Ok(UserManager.GetUsers(pageIndex ?? 1, pageSize ?? 10, name));
        }

        [AuthorizeRoles(Roles.Manager)]
        [Route("api/users/updateRoles")]
        [HttpPost]
        public IHttpActionResult UpdateRoles([FromBody] Dictionary<int, Roles> rolesToUpdate)
        {
            try
            {
                UserManager.UpdateRoles(rolesToUpdate);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}