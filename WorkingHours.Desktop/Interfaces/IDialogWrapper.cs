using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Interfaces
{
    public interface IDialogWrapper
    {
        void Ok(object input);

        void Cancel();
    }
}
