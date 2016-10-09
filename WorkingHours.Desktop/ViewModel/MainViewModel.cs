using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.ViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using WorkingHours.Client.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using WorkingHours.Desktop.Common;

namespace WorkingHours.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly ILoginManager loginManager;

        private string displayedName;

        public string DisplayedName
        {
            get { return displayedName; }

            set { Set(ref displayedName, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }

            set { Set(ref email, value); }
        }

        private bool isLoginVisible;
        public bool IsLoginVisible
        {
            get { return isLoginVisible; }

            set { Set(ref isLoginVisible, value); }
        }

        public ICommand LogoutCommand => new RelayCommand(ExecuteLogoutCommand);
        
        private string roles;
        public string Roles
        {
            get { return roles; }

            set { Set(ref roles, value); }
        }
        
        public MainViewModel(ILoginManager loginManager)
        {
            this.loginManager = loginManager;
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.LoginNotification, UpdateUserData);
            IsLoginVisible = true;
        }

        private void UpdateUserData(NotificationMessage obj)
        {
            Roles = string.Join(", ", loginManager.Roles.Select(x => x.ToString()));
            DisplayedName = $"{loginManager.FullName} ({loginManager.UserName})";
            Email = loginManager.Email;
            IsLoginVisible = false;
        }

        private void ExecuteLogoutCommand()
        {
            loginManager.Logout();
            IsLoginVisible = true;
        }
    }
}
