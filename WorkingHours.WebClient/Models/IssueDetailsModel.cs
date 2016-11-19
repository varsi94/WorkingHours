using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkingHours.Shared.Dto;

namespace WorkingHours.WebClient.Models
{
    public class IssueDetailsModel<TItem>
    {
        public PagedResult<TItem> WorkTimes { get; set; }

        public int ProjectId { get; set; }

        public int IssueId { get; set; }

        public string IssueName { get; set; }

        public bool IsManager { get; set; }
    }
}