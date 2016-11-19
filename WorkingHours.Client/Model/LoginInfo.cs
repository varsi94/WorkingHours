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
        public bool IsLoggedIn { get; set; }

        public int? Id { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string FullName { get; set; }
        
        public string Token { get; set; }
        
        public Roles? Role { get; set; }
    }
}
