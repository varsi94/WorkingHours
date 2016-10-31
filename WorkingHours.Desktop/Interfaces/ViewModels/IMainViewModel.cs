using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IMainViewModel : IViewModel
    {
        bool IsMainPageVisible { get; set; }

        bool IsLoginVisible { get; set; }

        bool IsSignUpVisible { get; set; }

        bool IsProjectControlVisible { get; set; }

        string DisplayedName { get; set; }

        Roles Role { get; set; }

        string Email { get; set; }

        ObservableCollection<ProjectHeader> MyProjects { get; set; }

        ICommand LogoutCommand { get; }

        ICommand ChangePasswordCommand { get; }

        ICommand ProjectSelectedCommand { get; }

        ICommand ShowManageUsersCommand { get; }
    }
}
