using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Desktop.Common;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.ViewModel
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private ILoginManager LoginManager { get; }

        public ICommand LoginCommand => new RelayCommand(ExecuteLoginCommand);

        private string password;

        public string Password
        {
            get { return password; }
            set { Set(ref password, value); }
        }

        private string username;

        public string UserName
        {
            get { return username; }
            set { Set(ref username, value); }
        }

        public LoginViewModel(ILoginManager loginManager)
        {
            LoginManager = loginManager;
        }

        private async void ExecuteLoginCommand()
        {
            var result = await LoginManager.LoginAsync(UserName, Password);
            if (!result)
            {
                MessageBox.Show("Nem sikerült a bejelentkezés!");
            }
            else
            {
                MessengerInstance.Send(new NotificationMessage(null), MessageTokens.LoginNotification);
            }
        }
    }
}
