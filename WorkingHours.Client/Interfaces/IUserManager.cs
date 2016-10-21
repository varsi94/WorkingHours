using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Client.Interfaces
{
    public interface IUserManager
    {
        Task<PagedResult<UserHeaderDto>> ListUsersAsync(int? pageSize = null, int? pageIndex = null, string nameFilter = null);

        Task UpdateRolesAsync(Dictionary<int, Roles> rolesToUpdate);
    }
}
