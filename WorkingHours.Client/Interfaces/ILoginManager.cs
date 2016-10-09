using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Interfaces
{
    public interface ILoginManager
    {
        bool IsLoggedIn { get; }

        string UserName { get; }

        string Email { get; }
        
        string FullName { get; }

        string Token { get; }

        Task<bool> Login(string username, string password);

        void Logout();

        bool IsManager { get; }

        bool IsEmployee { get; }
    }
}
