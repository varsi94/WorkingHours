using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Model;

namespace WorkingHours.Shared.Dto
{
    public class ProjectMemberDto : UserHeaderDto
    {
        private Roles roleInProjectEnum;

        public string RoleInProject
        {
            get { return roleInProjectEnum.ToString(); }
            set { roleInProjectEnum = (Roles)Enum.Parse(typeof(Roles), value); }
        }

        [JsonIgnore]
        public Roles RoleInPorjectEnum
        {
            get { return roleInProjectEnum; }
            set { roleInProjectEnum = value; }
        }
    }
}
