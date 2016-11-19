using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.WebClient.Models;

namespace WorkingHours.WebClient.Interfaces
{
    public interface ILoginManager
    {
        Task<ClaimsIdentity> LoginAsync(LoginModel loginModel);
    }
}
