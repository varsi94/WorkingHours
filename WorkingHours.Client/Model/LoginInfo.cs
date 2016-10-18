using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Model
{
    public class LoginInfo
    {
        public bool IsLoggedIn { get; internal set; }

        public string UserName { get; internal set; }
        
        public string Email { get; internal set; }
        
        public string FullName { get; internal set; }
        
        public string Token { get; internal set; }
        
        public bool IsManager => Roles.Any(x => x == Model.Roles.Manager);

        public bool IsEmployee => Roles.Any(x => x == Model.Roles.Employee);
        
        public IEnumerable<Roles> Roles { get; internal set; }
    }
}
