using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Bll.Model
{
    public class ReportData
    {
        public ApplicationUser User { get; set; }

        public Project Project { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<WorkTime> WorkTimeList { get; set; }
    }
}
