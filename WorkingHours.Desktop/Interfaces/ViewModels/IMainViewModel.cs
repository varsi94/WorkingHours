using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IMainViewModel : IViewModel
    {
        bool IsLoginVisible { get; set; }

        string DisplayedName { get; set; }
        
        string Roles { get; set; }

        string Email { get; set; }

        ICommand LogoutCommand { get; }
    }
}
