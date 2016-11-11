using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Interfaces
{
    public interface IUserManager
    {
        void CreateUser(ApplicationUser user, string password);

        void UpdateRoles(Dictionary<int, Roles> rolesToUpdate);

        void ChangePassword(int userId, string oldPassword, string newPassword);

        PagedResult<UserHeaderDto> GetUsers(PagingInfo pagingInfo, string nameFilter);
    }
}
