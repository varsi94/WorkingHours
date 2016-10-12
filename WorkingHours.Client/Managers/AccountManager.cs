﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Managers
{
    public class AccountManager : IAccountManager
    {
        private IEnumerable<Roles> roles;

        private IConfigurationManager ConfigurationManager { get; }

        public string Email { get; private set; }

        public string FullName { get; private set; }

        public bool IsLoggedIn { get; private set; }

        public string Token { get; private set; }

        public string UserName { get; private set; }

        public bool IsEmployee => roles.Any(x => x == Model.Roles.Employee);

        public bool IsManager => roles.Any(x => x == Model.Roles.Manager);

        public IEnumerable<Roles> Roles => roles;

        public AccountManager(IConfigurationManager configManager)
        {
            ConfigurationManager = configManager;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.ApiBaseAddress);
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

                Token = loginResult.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var userDataHttpResult = await client.GetAsync("api/account/whoami");
                if (userDataHttpResult.IsSuccessStatusCode)
                {
                    var userDataResult = await userDataHttpResult.Content.ReadAsAsync<UserDataResult>();
                    UserName = userDataResult.UserName;
                    Email = userDataResult.Email;
                    FullName = userDataResult.FullName;
                    roles = userDataResult.Roles.Select(x => (Roles) Enum.Parse(typeof(Roles), x));
                    return true;
                }

                return false;
            }
        }

        public void Logout()
        {
            if (!IsLoggedIn)
            {
                return;
            }

            IsLoggedIn = false;
            FullName = null;
            UserName = null;
            Token = null;
            Email = null;
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