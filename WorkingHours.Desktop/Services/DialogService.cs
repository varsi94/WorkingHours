using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkingHours.Desktop.Controls;
using WorkingHours.Desktop.Dialogs;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async Task<PasswordChangeModel> ShowPasswordChangeDialogAsync()
        {
            var dialogWrapper = new DialogWrapper<ChangePasswordControl, IChangePasswordViewModel>("Change password", 500, 280);
            var result = await dialogWrapper.ShowDialogAsync();
            if (result == null)
            {
                return null;
            }

            return new PasswordChangeModel
            {
                OldPassword = result.OldPassword,
                NewPassword = result.NewPassword
            };
        }
    }
}
