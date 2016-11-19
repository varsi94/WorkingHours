using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using WorkingHours.Shared.Model;

namespace WorkingHours.WebClient.Extensions
{
    public static class IdentityExtensions
    {
        public const string UserIdClaimType = "Id";
        public const string UserNameClaimType = "Username";
        public const string FullNameClaimType = ClaimTypes.Name;
        public const string TokenClaimType = "Token";
        public const string EmailClaimType = ClaimTypes.Email;
        public const string RoleClaimType = "Role";

        public static int? GetUserId(this IPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            return int.Parse((user.Identity as ClaimsIdentity).Claims.Single(x => x.Type == UserIdClaimType).Value);
        }

        public static string GetFullname(this IPrincipal user)
        {
            return (user.Identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == FullNameClaimType)?.Value;
        }

        public static string GetUsername(this IPrincipal user)
        {
            return (user.Identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == UserNameClaimType)?.Value;
        }

        public static string GetToken(this IPrincipal user)
        {
            return (user.Identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == TokenClaimType)?.Value;
        }

        public static string GetEmail(this IPrincipal user)
        {
            return (user.Identity as ClaimsIdentity).Claims.SingleOrDefault(x => x.Type == EmailClaimType)?.Value;
        }

        public static Roles? GetRole(this IPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            return (Roles)(Enum.Parse(typeof(Roles), (user.Identity as ClaimsIdentity).Claims.Single(x => x.Type == RoleClaimType).Value));
        }
    }
}