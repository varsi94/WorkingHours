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
        private IAccountManager AccountManager { get; }

        public ICommand LoginCommand { get; }

        public ICommand SignUpCommand { get; }

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

        public LoginViewModel(IAccountManager accountManager)
        {
            AccountManager = accountManager;
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand);
        }

        private void ExecuteSignUpCommand()
        {
            MessengerInstance.Send(new NotificationMessage(null), MessageTokens.StartSignUp);
        }

        private async void ExecuteLoginCommand()
        {
            var result = await AccountManager.LoginAsync(UserName, Password);
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
