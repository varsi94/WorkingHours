using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using WorkingHours.Model.UoW;
using WorkingHours.Model.DbContext;
using System.Security.Claims;

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

            var factory = new UoWFactory();
            ApplicationUser user = null;
            using (var uow = factory.GetUoW())
            {
                user = uow.Users.Get(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return Task.FromResult<object>(null);
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("FullName", user.FullName));
            //TODO
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
            return Task.FromResult<object>(null);
        }
    }
}