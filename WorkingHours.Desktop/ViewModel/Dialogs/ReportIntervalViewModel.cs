using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Common;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.ViewModel
{
    public class ReportIntervalViewModel : ViewModelBase, IReportIntervalViewModel
    {
        private DateTime? startDate;

        public DateTime? StartDate
        {
            get { return startDate; }

            set { Set(ref startDate, value); }
        }


        private DateTime? endDate;

        public DateTime? EndDate
        {
            get { return endDate; }

            set { Set(ref endDate, value); }
        }
    }
}
