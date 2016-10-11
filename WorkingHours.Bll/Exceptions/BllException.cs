using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Exceptions
{
    public abstract class BllException : Exception
    {
        public BllException(string message) : base(message)
        {
        }
    }
}
