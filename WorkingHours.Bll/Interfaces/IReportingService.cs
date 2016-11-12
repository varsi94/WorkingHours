using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Model;

namespace WorkingHours.Bll.Interfaces
{
    public interface IReportingService
    {
        byte[] GetProjectWorkTimeReport(ReportData reportData);
    }
}
