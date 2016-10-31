using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WorkingHours.Client.Common;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Client.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        public UserManager(IAppSettingsManager configurationManager) : base(configurationManager)
        {
        }

        public async Task<PagedResult<UserHeaderDto>> ListUsersAsync(int? pageSize = default(int?), int? pageIndex = default(int?), string nameFilter = null)
        {
            if (LoginInfo.Role != Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var param = new Dictionary<string, object>
                {
                    {nameof(pageSize), pageSize},
                    {nameof(pageIndex), pageIndex},
                    {"name", nameFilter}
                };
                var httpResult = await client.GetAsync("api/users/" + BuildQueryStirng(param));
                if (httpResult.IsSuccessStatusCode)
                {
                    var result = await httpResult.Content.ReadAsAsync<PagedResult<UserHeaderDto>>();
                    return result;
                }
                else if (httpResult.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    throw new Exception("Internal server error!");
                }
            }
        }

        public async Task UpdateRolesAsync(Dictionary<int, Roles> rolesToUpdate)
        {
            if (LoginInfo.Role != Roles.Manager)
            {
                throw new UnauthorizedAccessException();
            }

            using (var client = GetAuthenticatedClient())
            {
                var httpResult = await client.PostAsJsonAsync("api/users/updateRoles", rolesToUpdate);
                if (!httpResult.IsSuccessStatusCode)
                {
                    if (httpResult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var msg = await httpResult.Content.ReadAsAsync<ErrorMessage>();
                        throw new InvalidOperationException(msg.Message);
                    }
                    throw new Exception("Internal server error!");
                }
            }
        }
    }
}
