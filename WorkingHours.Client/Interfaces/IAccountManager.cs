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
        bool IsLoggedIn { get; }

        string UserName { get; }

        string Email { get; }
        
        string FullName { get; }

        string Token { get; }

        bool IsManager { get; }

        bool IsEmployee { get; }

        IEnumerable<Roles> Roles { get; }
        
        Task<bool> LoginAsync(string username, string password);

        void Logout();

        Task SignUpAsync(SignUpRequest request);
    }
}
