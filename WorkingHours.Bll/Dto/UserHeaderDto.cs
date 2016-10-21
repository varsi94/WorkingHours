using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;

namespace WorkingHours.Bll.Dto
{
    public class UserHeaderDto
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Roles Role { get; set; }
    }
}
