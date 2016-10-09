using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace WorkingHours.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }

        public static string GetFullName(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == "FullName").Value;
        }

        public static IEnumerable<string> GetRoles(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
    }
}