using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Shared.Dto;

namespace WorkingHours.WebClient.Models
{
    public class ProjectDetailsModel
    {
        public List<IssueHeader> Issues { get; set; }

        public int ProjectId { get; set; }
    }
}