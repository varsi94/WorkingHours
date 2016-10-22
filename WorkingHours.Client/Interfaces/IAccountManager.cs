using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Model;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Client.Interfaces
{
    public interface IAccountManager : IManager
    {
        Task<bool> LoginAsync(string username, string password);

        void Logout();

        Task SignUpAsync(SignUpModel request);

        Task ChangePasswordAsync(PasswordChangeModel request);
    }
}
