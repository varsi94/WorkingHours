using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface ILoginViewModel : IViewModel
    {
        ICommand LoginCommand { get; }

        string UserName { get; set; }

        string Password { get; set; }
    }
}
