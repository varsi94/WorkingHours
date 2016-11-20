using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Desktop.ViewModel;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IWorkTimesViewModel : ITabViewModel
    {
        ObservableCollection<IssueViewModel> Issues { get; }

        ObservableCollection<WorkTimeViewModel> WorkTimes { get; }

        ICommand GenerateReportCommand { get; }

        ICommand SaveCommand { get; }

        ICommand DiscardChangedCommand { get; }
        
        IssueViewModel SelectedIssue { get; set; }

        WorkTimeViewModel CurrentWorkTime { get; set; }

        bool IsDetailsVisible { get; }
    }
}
