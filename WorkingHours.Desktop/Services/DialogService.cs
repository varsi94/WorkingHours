using Microsoft.Win32;
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
        public async Task<INewProjectViewModel> ShowAddProjectDialogAsync()
        {
            var dialogWrapper = new DialogWrapper<AddProjectControl, INewProjectViewModel>("Add new project", 500, 280);
            return await dialogWrapper.ShowDialogAsync();
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowManageUsersWindow()
        {
            var window = new ManageUsersWindow();
            window.Show();
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

        public async Task<IReportIntervalViewModel> ShowReportIntervalDialogAsync()
        {
            var dialogWrapper = new DialogWrapper<ReportIntervalDialogControl, IReportIntervalViewModel>("Report interval", 500, 280);
            return await dialogWrapper.ShowDialogAsync();
        }

        public string ShowSaveFileDialog(string title, Dictionary<string, string> extensions)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = title,
                AddExtension = true,
                DefaultExt = extensions.First().Value,
                Filter = string.Join("|", extensions.Select(x => $"{x.Key}|{x.Value}"))
            };
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
