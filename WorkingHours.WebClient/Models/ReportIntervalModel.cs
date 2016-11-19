using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkingHours.WebClient.Models
{
    public class ReportIntervalModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        public int Id { get; set; }
    }
}