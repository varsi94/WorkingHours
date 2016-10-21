using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Model;

namespace WorkingHours.Client.Model
{
    public class LoginInfo
    {
        public bool IsLoggedIn { get; internal set; }

        public string UserName { get; internal set; }
        
        public string Email { get; internal set; }
        
        public string FullName { get; internal set; }
        
        public string Token { get; internal set; }
        
        public Roles? Role { get; internal set; }
    }
}
