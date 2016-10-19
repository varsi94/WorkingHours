using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Common;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Managers
{
    public class AccountManager : ManagerBase, IAccountManager
    {
        public AccountManager(LoginInfo loginInfo, IAppSettingsManager configManager) : base(loginInfo, configManager)
        {
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync("api/account/changePassword", request);
                if (!httpResult.IsSuccessStatusCode)
                {
                    if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new ModelStateException("New password must be at least 6 characters long!", await httpResult.Content.ReadAsAsync<ModelState>());
                    }
                    else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ServerException("Old password is not correct!");
                    }
                }
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            using (var client = GetBasicClient())
            {
                var data = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", username},
                    {"password", password}
                };
                var httpResult = await client.PostAsync("api/token", new FormUrlEncodedContent(data));
                var loginResult = await httpResult.Content.ReadAsAsync<LoginResult>();

                if (loginResult.AccessToken == null)
                {
                    return false;
                }

                LoginInfo.Token = loginResult.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginInfo.Token);
                var userDataHttpResult = await client.GetAsync("api/account/whoami");
                if (userDataHttpResult.IsSuccessStatusCode)
                {
                    var userDataResult = await userDataHttpResult.Content.ReadAsAsync<UserDataResult>();
                    LoginInfo.UserName = userDataResult.UserName;
                    LoginInfo.Email = userDataResult.Email;
                    LoginInfo.FullName = userDataResult.FullName;
                    LoginInfo.Roles = userDataResult.Roles.Select(x => (Roles) Enum.Parse(typeof(Roles), x));
                    return true;
                }

                return false;
            }
        }

        public void Logout()
        {
            if (!LoginInfo.IsLoggedIn)
            {
                return;
            }

            LoginInfo.IsLoggedIn = false;
            LoginInfo.FullName = null;
            LoginInfo.UserName = null;
            LoginInfo.Token = null;
            LoginInfo.Email = null;
        }

        public async Task SignUpAsync(SignUpRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.ApiBaseAddress);
                var postResult = await client.PostAsJsonAsync("api/account/signup", request);
                if (postResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ModelStateException("Sign up failed!", await postResult.Content.ReadAsAsync<ModelState>());
                }
                else if (postResult.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ServerException("User name is already taken!");
                }
            }
        }
    }
}
