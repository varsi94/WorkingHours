using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface ISignUpViewModel : IViewModel
    {
        ICommand SignUpCommand { get; }
        
        ICommand BackToLoginCommand { get; }

        string UserName { get; set; }

        string Password { get; set; }

        string PasswordConfirmed { get; set; }

        string Email { get; set; }

        string FullName { get; set; }
    }
}
