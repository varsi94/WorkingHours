using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IChangePasswordViewModel : IViewModel
    {
        string OldPassword { get; set; }

        string NewPassword { get; set; }

        string NewPasswordConfirmed { get; set; }
    }
}
