using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.ViewModels;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using WorkingHours.Shared.Dto;
using WorkingHours.Desktop.Common;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using WorkingHours.Client.Exceptions;
using WorkingHours.Desktop.Interfaces.Services;

namespace WorkingHours.Desktop.ViewModel
{
    public class ManageIssuesViewModel : ViewModelBase, IManageIssuesViewModel
    {
        private readonly IDialogService dialogService;
        private readonly ILoadingService loadingService;
        private ProjectInfo project;
        private bool isNew = false;
        private readonly IIssueManager issueManager;
        private ObservableCollection<IssueViewModel> issues;

        public ObservableCollection<IssueViewModel> Issues
        {
            get { return issues; }
            protected set { Set(ref issues, value); }
        }


        private IssueViewModel selectedIssue;

        public IssueViewModel SelectedIssue
        {
            get { return selectedIssue; }

            set { Set(ref selectedIssue, value); }
        }


        private bool isEditorVisible;

        public bool IsEditorVisible
        {
            get { return isEditorVisible; }

            set { Set(ref isEditorVisible, value); }
        }

        public ICommand SaveCommand { get; }

        public ICommand NewIssueCommand { get; }

        public ICommand IssueSelectedCommand { get; }

        public ICommand DiscardChangesCommand { get; }

        public ManageIssuesViewModel(IIssueManager issueManager, ILoadingService loadingService, IDialogService dialogService)
        {
            MessengerInstance.Register<NotificationMessage<ProjectInfo>>(this, MessageTokens.ReceiveIssuesToken, ReceiveIssues);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            NewIssueCommand = new RelayCommand(ExecuteNewIssueCommand);
            IssueSelectedCommand = new RelayCommand<IssueViewModel>(ExecuteIssueSelectedCommand);
            DiscardChangesCommand = new RelayCommand(ExecuteDiscardChangesCommand);
            this.issueManager = issueManager;
            this.loadingService = loadingService;
            this.dialogService = dialogService;
        }

        private void ExecuteDiscardChangesCommand()
        {
            SelectedIssue.CancelEdit();
            if (isNew)
            {
                SelectedIssue = null;
                IsEditorVisible = false;
            }
            else
            {
                SelectedIssue.BeginEdit();
            }
        }

        private void ExecuteIssueSelectedCommand(IssueViewModel obj)
        {
            if (obj == null)
            {
                IsEditorVisible = false;
                return;
            }

            if (SelectedIssue != null)
            {
                SelectedIssue.CancelEdit();
            }
            SelectedIssue = obj;
            IsEditorVisible = true;
            SelectedIssue.BeginEdit();
            isNew = false;
        }

        private void ExecuteNewIssueCommand()
        {
            SelectedIssue = new IssueViewModel(new IssueHeader() {Deadline = DateTime.Now, IsClosed = false, Name = "New issue"});
            isNew = true;
            IsEditorVisible = true;
        }

        private async void ExecuteSaveCommand()
        {
            try
            {
                loadingService.ShowIndicator("Saving...");
                if (isNew)
                {
                    await issueManager.CreateIssueForProjectAsync(project.Id, SelectedIssue.GetIssueHeader());
                }
                else
                {
                    await issueManager.UpdateIssueAsync(SelectedIssue.GetIssueHeader());
                    SelectedIssue.EndEdit();
                }
                MessengerInstance.Send(new NotificationMessage(null), MessageTokens.ReloadProjectToken);
            }
            catch (ServerException)
            {
                dialogService.ShowError("Error during save!",
                    "There is a conflicting edit! Reload the issue, and update it again!");
            }
            finally
            {
                loadingService.HideIndicator();
            }
        }

        private void ReceiveIssues(NotificationMessage<ProjectInfo> obj)
        {
            project = obj.Content;
            Issues = new ObservableCollection<IssueViewModel>(obj.Content.Issues.Select(x => new IssueViewModel(x)));
        }
    }
}
