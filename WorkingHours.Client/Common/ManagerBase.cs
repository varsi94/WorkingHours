using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Common
{
    public abstract class ManagerBase
    {
        protected LoginInfo LoginInfo { get; }

        protected IAppSettingsManager ConfigurationManager { get; }

        public ManagerBase(LoginInfo loginInfo, IAppSettingsManager configurationManager)
        {
            LoginInfo = loginInfo;
            ConfigurationManager = configurationManager;
        }
        
        protected virtual HttpClient GetAuthenticatedClient()
        {
            if (!LoginInfo.IsLoggedIn)
            {
                throw new UnauthorizedAccessException();
            }

            var client = new HttpClient() {BaseAddress = new Uri(ConfigurationManager.ApiBaseAddress)};
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer", LoginInfo.Token);
            return client;
        }

        protected virtual HttpClient GetBasicClient()
        {
            return new HttpClient {BaseAddress = new Uri(ConfigurationManager.ApiBaseAddress)};
        }

        protected string BuildQueryStirng(Dictionary<string, object> param)
        {
            var stringBuilder = new StringBuilder();
            bool isFirst = true;
            foreach (var item in param)
            {
                if (item.Value == null)
                {
                    continue;
                }

                if (isFirst)
                {
                    stringBuilder.Append("?");
                    isFirst = false;
                }
                else
                {
                    stringBuilder.Append("&");
                }

                stringBuilder.Append(item.Key + "=" + item.Value);
            }

            return stringBuilder.ToString();
        }
    }
}
