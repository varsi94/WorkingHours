using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;

namespace WorkingHours.Client.Managers
{
    public class AppSettingsManager : IConfigurationManager
    {
        public string ApiBaseAddress => ConfigurationManager.AppSettings["apiBaseAddress"];
    }
}
