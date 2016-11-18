using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Interfaces
{
    public interface IServerConfigurationManager
    {
        int WorkTimeUpdateIntervalInDays { get; }
    }
}
