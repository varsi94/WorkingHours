using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.ViewModel
{
    public class WorkTimesViewModel : TabViewModelBase, IWorkTimesViewModel
    {
        private readonly IWorkTimeManager workTimeManager;

        public WorkTimesViewModel(LoginInfo loginInfo, IWorkTimeManager workTimeManager) : base(loginInfo)
        {
            this.workTimeManager = workTimeManager;
        }

        private WorkTimeViewModel currentWorkTime;

        public WorkTimeViewModel CurrentWorkTime
        {
            get { return currentWorkTime; }
            set { Set(ref currentWorkTime, value); }
        }


        public ICommand DiscardChangedCommand { get; }

        public ICommand GenerateReportCommand { get; }


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
            return base.OnProjectChanged();
        }
    }
}
