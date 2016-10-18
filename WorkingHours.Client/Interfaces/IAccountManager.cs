using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> LoginAsync(string username, string password);

        void Logout();

        Task SignUpAsync(SignUpRequest request);

        Task ChangePasswordAsync(ChangePasswordRequest request);
    }
}
