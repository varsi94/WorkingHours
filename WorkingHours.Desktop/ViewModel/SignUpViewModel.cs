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
using WorkingHours.Desktop.Messaging;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Services;
using System.ComponentModel.DataAnnotations;
using WorkingHours.Shared.Dto;
using WorkingHours.Desktop.Common;

namespace WorkingHours.Desktop.ViewModel
{
    public class SignUpViewModel : ValidatableViewModelBase, ISignUpViewModel
    {
        private IAccountManager AccountManager { get; }

        private IDialogService DialogService { get; }

        private ILoadingService LoadingService { get; }

        private string email;

        [Required(ErrorMessage = "Email address is required!")]
        [EmailAddress(ErrorMessage = "Email address is not valid!")]
        public string Email
        {
            get { return email; }

            set { Set(ref email, value); }
        }

        private string fullName;

        [Required(ErrorMessage = "Full name is required!")]
        public string FullName
        {
            get { return fullName; }

            set { Set(ref fullName, value); }
        }

        private string password;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100!")]
        public string Password
        {
            get { return password; }

            set { Set(ref password, value); }
        }

        private string passwordConfirmed;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match each other!")]
        public string PasswordConfirmed
        {
            get { return passwordConfirmed; }

            set { Set(ref passwordConfirmed, value); }
        }

        public ICommand SignUpCommand { get; }

        public ICommand BackToLoginCommand { get; }

        private string userName;

        [Required(ErrorMessage = "User name is required!")]
        public string UserName
        {
            get { return userName; }

            set { Set(ref userName, value); }
        }

        public SignUpViewModel(IAccountManager accountManager, IDialogService dialogService, ILoadingService loadingService) : base()
        {
            AccountManager = accountManager;
            DialogService = dialogService;
            LoadingService = loadingService;
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand);
            BackToLoginCommand = new RelayCommand(ExecuteBackToLoginCommand);
        }

        private void ExecuteBackToLoginCommand()
        {
            MessengerInstance.Send(new NotificationMessage(null), MessageTokens.SignUpCompleted);
            UserName = string.Empty;
            Password = string.Empty;
            PasswordConfirmed = string.Empty;
            Email = string.Empty;
            FullName = string.Empty;
        }

        private async void ExecuteSignUpCommand()
        {
            var signUp = new SignUpModel
            {
                UserName = UserName,
                Password = Password,
                Email = Email,
                FullName = FullName
            };

            try
            {
                LoadingService.ShowIndicator("Signing up...");
                await AccountManager.SignUpAsync(signUp);
                MessengerInstance.Send(new NotificationMessage(null), MessageTokens.SignUpCompleted);
                Clear();
            }
            catch (ModelStateException)
            {
                DialogService.ShowError("Signup error", "Error occured during signup!");
            }
            catch (ServerException)
            {
                DialogService.ShowError("Signup error", "Username is already taken! Please choose another one!");
            }
            finally
            {
                LoadingService.HideIndicator();
            }
        }

        private void Clear()
        {
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            FullName = string.Empty;
            PasswordConfirmed = string.Empty;
        }

        protected override void FillInProperties()
        {
            PropertiesToValidate.Add(nameof(Email), () => Email);
            PropertiesToValidate.Add(nameof(UserName), () => UserName);
            PropertiesToValidate.Add(nameof(Password), () => Password);
            PropertiesToValidate.Add(nameof(PasswordConfirmed), () => PasswordConfirmed);
            PropertiesToValidate.Add(nameof(FullName), () => FullName);
        }
    }
}
