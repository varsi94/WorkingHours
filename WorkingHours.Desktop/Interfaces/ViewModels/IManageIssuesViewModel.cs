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
    public interface IManageIssuesViewModel : IViewModel
    {
        bool IsEditorVisible { get; set; }

        ObservableCollection<IssueViewModel> Issues { get; }

        IssueViewModel SelectedIssue { get; set; }

        ICommand SaveCommand { get; }

        ICommand NewIssueCommand { get; }

        ICommand IssueSelectedCommand { get; }

        ICommand DiscardChangesCommand { get; }
    }
}
