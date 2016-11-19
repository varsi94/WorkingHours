using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Shared.Dto;

namespace WorkingHours.WebClient.Models
{
    public class IssueDetailsModel
    {
        public PagedResult<WorkTimeDto> WorkTimes { get; set; }

        public int ProjectId { get; set; }

        public string IssueName { get; set; }
    }
}