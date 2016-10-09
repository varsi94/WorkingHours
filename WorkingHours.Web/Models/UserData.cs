using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingHours.Web.Models
{
    public class UserData
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}