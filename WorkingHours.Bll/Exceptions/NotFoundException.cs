using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Exceptions
{
    public class NotFoundException : BllException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
