using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WorkingHours.Bll.Interfaces;

namespace WorkingHours.Web.Managers
{
    public class ServerConfigurationManager : IServerConfigurationManager
    {
        public int WorkTimeUpdateIntervalInDays
        {
            get { return int.Parse(ConfigurationManager.AppSettings[nameof(WorkTimeUpdateIntervalInDays)]); }
        }
    }
}