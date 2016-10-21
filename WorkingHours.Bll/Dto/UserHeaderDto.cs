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
        private Roles role;

        public int Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role
        {
            get { return role.ToString(); }
            set { role = (Roles)Enum.Parse(typeof(Roles), value); }
        }
    }
}
