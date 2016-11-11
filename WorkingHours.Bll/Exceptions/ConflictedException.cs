using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Exceptions
{
    public class ConflictedException : BllException
    {
        public ConflictedException(string message) : base(message)
        {
        }
    }
}
