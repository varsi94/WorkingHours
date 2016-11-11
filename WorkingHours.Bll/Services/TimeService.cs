using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Interfaces;

namespace WorkingHours.Bll.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
