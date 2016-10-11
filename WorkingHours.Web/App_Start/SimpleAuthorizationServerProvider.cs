using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using WorkingHours.Model.UoW;
using WorkingHours.Model.DbContext;
using System.Security.Claims;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WorkingHours.Web.App_Start
{
    internal class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {   
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            ApplicationUser user = null;
            IList<string> roles = null;
            using (var ctx = new AppDbContext())
            {
                var uow = new UnitOfWork(ctx);
                user = uow.Users.Get(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return Task.FromResult<object>(null);
                }

                roles = uow.Users.GetRoles(user);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("FullName", user.FullName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            context.Validated(identity);
            return Task.FromResult<object>(null);
        }
    }
}