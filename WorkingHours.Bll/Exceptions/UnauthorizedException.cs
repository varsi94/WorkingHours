using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Exceptions
{
    public class UnauthorizedException : BllException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
