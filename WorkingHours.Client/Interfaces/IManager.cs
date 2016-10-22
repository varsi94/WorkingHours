using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Interfaces
{
    public interface IManager
    {
        LoginInfo LoginInfo { get; set; }
    }
}
