using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Model;

namespace WorkingHours.Client.Exceptions
{
    public class ModelStateException : Exception
    {
        public ModelState ModelState { get; set; }

        public ModelStateException(string message, ModelState modelState) : base(message)
        {
            ModelState = modelState;
        }
    }
}
