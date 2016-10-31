using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.Interfaces.Services
{
    public interface IDialogService
    {
        void ShowError(string title, string message);

        Task<PasswordChangeModel> ShowPasswordChangeDialogAsync();

        Task<INewProjectViewModel> ShowAddProjectDialogAsync();

        void ShowManageUsersWindow();
    }
}
