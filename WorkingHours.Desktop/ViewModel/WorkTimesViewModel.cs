using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.ViewModel
{
    public class WorkTimesViewModel : TabViewModelBase, IWorkTimesViewModel
    {
        private int selectedIssueId = -1;
        private const int PageSize = 20;
        private readonly IWorkTimeManager workTimeManager;
        private readonly ILoadingService loadingService;
        private bool isNew = false;

        public WorkTimesViewModel(LoginInfo loginInfo, IWorkTimeManager workTimeManager, ILoadingService loadingService) : base(loginInfo)
        {
            this.workTimeManager = workTimeManager;
            this.loadingService = loadingService;
            NewWorkTimeCommand = new RelayCommand(ExecuteNewWorkTimeCommand);
            DiscardChangesCommand = new RelayCommand(ExecuteDiscardChangesCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
        }

        private WorkTimeViewModel currentWorkTime;

        public WorkTimeViewModel CurrentWorkTime
        {
            get { return currentWorkTime; }
            set
            {
                if (!isNew && CurrentWorkTime != null)
                {
                    CurrentWorkTime.CancelEdit();
                }

                Set(ref currentWorkTime, value);
                IsDetailsVisible = currentWorkTime != null;
                isNew = false;

                if (CurrentWorkTime != null)
                {
                    CurrentWorkTime.BeginEdit();
                }
            }
        }

        private bool isDetailsVisible;

        public bool IsDetailsVisible
        {
            get { return isDetailsVisible; }

            set { Set(ref isDetailsVisible, value); }
        }

        public ICommand DiscardChangesCommand { get; }

        public ICommand GenerateReportCommand { get; }

        public ICommand NewWorkTimeCommand { get; }

        private IssueViewModel selctedIssue;

        public IssueViewModel SelectedIssue
        {
            get { return selctedIssue; }

            set
            {
                Set(ref selctedIssue, value);
                if (value != null)
                {
                    UpdateWorkTimes();
                }
                else
                {
                    WorkTimes = new ObservableCollection<WorkTimeViewModel>();
                    IsDetailsVisible = false;
                    CurrentWorkTime = null;
                }
            }
        }

        private ObservableCollection<IssueViewModel> issues;
        public ObservableCollection<IssueViewModel> Issues
        {
            get { return issues; }

            protected set { Set(ref issues, value); }
        }

        public ICommand SaveCommand { get; }

        private ObservableCollection<WorkTimeViewModel> workTimes;
        public ObservableCollection<WorkTimeViewModel> WorkTimes
        {
            get { return workTimes; }
            protected set { Set(ref workTimes, value); }
        }

        protected override Task OnProjectChanged()
        {
            Issues = new ObservableCollection<IssueViewModel>(CurrentProject.Issues.Select(x => new IssueViewModel(x)));
            SelectedIssue = (selectedIssueId == -1) ? Issues.FirstOrDefault() : Issues.FirstOrDefault(x => x.Id == selectedIssueId);
            return base.OnProjectChanged();
        }
        
        private async void UpdateWorkTimes()
        {
            loadingService.ShowIndicator("Loading worktime...");
            var result = await workTimeManager.GetMyWorkTimesAsync(SelectedIssue.Id, PageSize, 1);
            WorkTimes = new ObservableCollection<WorkTimeViewModel>(result.Items.Select(x => new WorkTimeViewModel(x)));
            loadingService.HideIndicator();
            CurrentWorkTime = null;
            IsDetailsVisible = false;
            IsWriteable = CurrentProject.IsWriteable && !CurrentProject.IsClosed && !(SelectedIssue?.IsClosed ?? true);
        }
        
        private void ExecuteNewWorkTimeCommand()
        {
            CurrentWorkTime = new NewWorkTimeViewModel(new Shared.Dto.WorkTimeDto
            {
                Name = "New worktime",
                Hours = 1,
                Date = DateTime.Today
            });
            isNew = true;
        }

        private void ExecuteDiscardChangesCommand()
        {
            CurrentWorkTime.CancelEdit();
            if (isNew)
            {
                CurrentWorkTime = null;
                IsDetailsVisible = false;
                isNew = false;
            }
        }
        
        private async void ExecuteSaveCommand()
        {
            if (isNew)
            {
                selectedIssueId = SelectedIssue.Id;
                await workTimeManager.AddWorkTimeAsync(SelectedIssue.Id, CurrentWorkTime.WorkTimeDto);
                ReloadProject();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
