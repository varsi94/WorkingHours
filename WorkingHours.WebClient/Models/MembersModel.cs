using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Shared.Dto;

namespace WorkingHours.WebClient.Models
{
    public class MembersModel
    {
        public List<ProjectMemberDto> Members { get; set; }

        public string ProjectName { get; set; }
    }
}