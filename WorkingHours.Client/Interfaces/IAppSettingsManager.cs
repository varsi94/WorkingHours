using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Interfaces
{
    public interface IAppSettingsManager
    {
        string ApiBaseAddress { get; }
    }
}
