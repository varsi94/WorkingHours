using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WorkingHours.Model;

namespace WorkingHours.Web.Extensions
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Roles[] roles)
        {
            Roles = string.Join(", ", roles.Select(x => x.ToString()));
        }
    }
}