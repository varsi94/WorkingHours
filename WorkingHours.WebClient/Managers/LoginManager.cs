using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.WebClient.Extensions;
using WorkingHours.WebClient.Interfaces;
using WorkingHours.WebClient.Models;

namespace WorkingHours.WebClient.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly IAccountManager accountManager;

        public LoginManager(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
            accountManager.LoginInfo = new LoginInfo();
        }

        public async Task<ClaimsIdentity> LoginAsync(LoginModel loginModel)
        {
            var result = await accountManager.LoginAsync(loginModel.Username, loginModel.Password);
            if (!result)
            {
                return null;
            }

            var loginInfo = accountManager.LoginInfo;
            var identity = new ClaimsIdentity(new []
            {
                new Claim(IdentityExtensions.UserIdClaimType, loginInfo.Id.ToString()),
                new Claim(IdentityExtensions.FullNameClaimType, loginInfo.FullName),
                new Claim(IdentityExtensions.UserNameClaimType, loginInfo.UserName),
                new Claim(IdentityExtensions.RoleClaimType, loginInfo.Role.Value.ToString()),
                new Claim(IdentityExtensions.TokenClaimType, loginInfo.Token),
                new Claim(IdentityExtensions.EmailClaimType, loginInfo.Email)
            }, "ApplicationCookie");
            return identity;
        }
    }
}