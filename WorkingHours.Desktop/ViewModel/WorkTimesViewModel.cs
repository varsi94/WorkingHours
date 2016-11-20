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
        private const int PageSize = 20;
        private readonly IWorkTimeManager workTimeManager;
        private readonly ILoadingService loadingService;

        public WorkTimesViewModel(LoginInfo loginInfo, IWorkTimeManager workTimeManager, ILoadingService loadingService) : base(loginInfo)
        {
            this.workTimeManager = workTimeManager;
            this.loadingService = loadingService;
        }

        private WorkTimeViewModel currentWorkTime;

        public WorkTimeViewModel CurrentWorkTime
        {
            get { return currentWorkTime; }
            set
            {
                Set(ref currentWorkTime, value);
                IsDetailsVisible = currentWorkTime != null;
            }
        }


        private bool isDetailsVisible;

        public bool IsDetailsVisible
        {
            get { return isDetailsVisible; }

            set { Set(ref isDetailsVisible, value); }
        }

        public ICommand DiscardChangedCommand { get; }

        public ICommand GenerateReportCommand { get; }
        
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
            SelectedIssue = Issues.FirstOrDefault();
            return base.OnProjectChanged();
        }
        
        private async void UpdateWorkTimes()
        {
            loadingService.ShowIndicator("Loading worktime...");
            var result = await workTimeManager.GetMyWorkTimesAsync(SelectedIssue.Id, PageSize, 1);
            WorkTimes = new ObservableCollection<WorkTimeViewModel>(result.Items.Select(x => new WorkTimeViewModel(x)));
            loadingService.HideIndicator();
        }
    }
}
