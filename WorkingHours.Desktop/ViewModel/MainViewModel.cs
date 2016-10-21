﻿using GalaSoft.MvvmLight;
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
using WorkingHours.Client.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly LoginInfo loginInfo;
        private readonly IAccountManager accountManager;
        
        private bool isMainPageVisible;

        public bool IsMainPageVisible
        {
            get { return isMainPageVisible; }

            set { Set(ref isMainPageVisible, value); }
        }
        
        private bool isSignUpVisible;

        public bool IsSignUpVisible
        {
            get { return isSignUpVisible; }

            set { Set(ref isSignUpVisible, value); }
        }

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

        public ICommand LogoutCommand { get; }
        
        private string roles;
        public string Roles
        {
            get { return roles; }

            set { Set(ref roles, value); }
        }
        
        public MainViewModel(LoginInfo loginInfo, IAccountManager accountManager)
        {
            this.accountManager = accountManager;
            this.loginInfo = loginInfo;
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.LoginNotification, UpdateUserData);
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.SignUpCompleted, ExecuteSignUpCompleted);
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.StartSignUp, ExecuteStartSignUp);
            IsLoginVisible = true;

            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
        }

        private void ExecuteStartSignUp(NotificationMessage obj)
        {
            IsLoginVisible = false;
            IsSignUpVisible = true;
        }

        private void ExecuteSignUpCompleted(NotificationMessage obj)
        {
            IsLoginVisible = true;
            IsSignUpVisible = false;
        }

        private void UpdateUserData(NotificationMessage obj)
        {
            Roles = loginInfo.Role.ToString();
            DisplayedName = $"{loginInfo.FullName} ({loginInfo.UserName})";
            Email = loginInfo.Email;
            IsLoginVisible = false;
            IsMainPageVisible = true;
        }

        private void ExecuteLogoutCommand()
        {
            accountManager.Logout();
            IsLoginVisible = true;
        }
    }
}
