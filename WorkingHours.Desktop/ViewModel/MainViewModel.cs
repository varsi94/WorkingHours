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
using WorkingHours.Shared.Dto;
using System.Collections.ObjectModel;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Client.Exceptions;

namespace WorkingHours.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly ILoadingService loadingService;
        private readonly LoginInfo loginInfo;
        private readonly IAccountManager accountManager;
        private readonly IProjectManager projectmanager;
        private readonly IDialogService dialogService;

        private bool isMainPageVisible;
        private ProjectHeader selectedProject;

        public bool IsMainPageVisible
        {
            get { return isMainPageVisible; }

            set { Set(ref isMainPageVisible, value); }
        }


        private bool isProjectControlVisible;

        public bool IsProjectControlVisible
        {
            get { return isProjectControlVisible; }

            set { Set(ref isProjectControlVisible, value); }
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

        private ObservableCollection<ProjectHeader> myProjects;
        public ObservableCollection<ProjectHeader> MyProjects
        {
            get { return myProjects; }
            set { Set(ref myProjects, value); }
        }

        public ICommand ProjectSelectedCommand { get; }

        public ICommand ChangePasswordCommand { get; }

        public MainViewModel(LoginInfo loginInfo, IAccountManager accountManager, IProjectManager projectManager,
            IDialogService dialogService, ILoadingService loadingService)
        {
            this.accountManager = accountManager;
            this.loginInfo = loginInfo;
            this.loadingService = loadingService;
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.LoginNotification, UpdateUserData);
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.SignUpCompleted, ExecuteSignUpCompleted);
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.StartSignUp, ExecuteStartSignUp);
            IsLoginVisible = true;

            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
            ChangePasswordCommand = new RelayCommand(ExecuteChangePasswordCommand);
            ProjectSelectedCommand = new RelayCommand<ProjectHeader>(ExecuteProjectSelectedCommand);

            this.projectmanager = projectManager;
            this.dialogService = dialogService;
        }

        private async void ExecuteChangePasswordCommand()
        {
            var result = await dialogService.ShowPasswordChangeDialogAsync();
            if (result != null)
            {
                try
                {
                    loadingService.ShowIndicator("Changing password...");
                    await accountManager.ChangePasswordAsync(result);
                }
                catch (ServerException e)
                {
                    dialogService.ShowError("Error occured!", e.Message);
                }
                finally
                {
                    loadingService.HideIndicator();
                }
            }
        }

        private void ExecuteProjectSelectedCommand(ProjectHeader obj)
        {
            selectedProject = obj;
            IsProjectControlVisible = obj != null;
            MessengerInstance.Send(new NotificationMessage<ProjectHeader>(obj, null),
                MessageTokens.CurrentProjectChanged);
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

        private async void UpdateUserData(NotificationMessage obj)
        {
            Roles = loginInfo.Role.ToString();
            DisplayedName = $"{loginInfo.FullName} ({loginInfo.UserName})";
            Email = loginInfo.Email;
            MyProjects = new ObservableCollection<ProjectHeader>(await projectmanager.GetMyProjectsAsync());
            IsLoginVisible = false;
            IsMainPageVisible = true;
        }

        private void ExecuteLogoutCommand()
        {
            accountManager.Logout();
            IsLoginVisible = true;
            IsMainPageVisible = false;
        }
    }
}
