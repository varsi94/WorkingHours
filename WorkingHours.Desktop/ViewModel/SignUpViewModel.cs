using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.ViewModels;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using WorkingHours.Client.Model;
using WorkingHours.Client.Exceptions;
using GalaSoft.MvvmLight.Messaging;
using WorkingHours.Desktop.Common;
using WorkingHours.Desktop.Interfaces.Services;

namespace WorkingHours.Desktop.ViewModel
{
    public class SignUpViewModel : ViewModelBase, ISignUpViewModel
    {
        private IAccountManager AccountManager { get; }
        private IDialogService DialogService { get; }

        private string email;
        public string Email
        {
            get { return email; }

            set { Set(ref email, value); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }

            set { Set(ref fullName, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }

            set { Set(ref password, value); }
        }

        private string passwordConfirmed;
        public string PasswordConfirmed
        {
            get { return passwordConfirmed; }

            set { Set(ref passwordConfirmed, value); }
        }

        public ICommand SignUpCommand { get; }

        public ICommand BackToLoginCommand { get; }

        private string userName;
        public string UserName
        {
            get { return userName; }

            set { Set(ref userName, value); }
        }

        public SignUpViewModel(IAccountManager accountManager, IDialogService dialogService)
        {
            AccountManager = accountManager;
            DialogService = dialogService;
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand);
            BackToLoginCommand = new RelayCommand(ExecuteBackToLoginCommand);
        }

        private void ExecuteBackToLoginCommand()
        {
            MessengerInstance.Send(new NotificationMessage(null), MessageTokens.SignUpCompleted);
        }

        private async void ExecuteSignUpCommand()
        {
            var signUp = new SignUpRequest
            {
                UserName = UserName,
                Password = Password,
                Email = Email,
                FullName = FullName
            };

            try
            {
                await AccountManager.SignUpAsync(signUp);
                MessengerInstance.Send(new NotificationMessage(null), MessageTokens.SignUpCompleted);
            }
            catch (ModelStateException)
            {
                DialogService.ShowError("Signup error", "Error occured during signup!");
            }
            catch (ServerException)
            {
                DialogService.ShowError("Signup error", "Username is already taken! Please choose another one!");
            }
        }
    }
}
