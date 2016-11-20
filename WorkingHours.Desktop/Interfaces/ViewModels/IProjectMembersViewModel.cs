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
    public interface IProjectMembersViewModel : ITabViewModel
    {
        List<UserViewModel> SearchResults { get; }

        UserViewModel SelectedUser { get; set; }

        ICommand SearchCommand { get; }

        ICommand AddCommand { get; }

        ICommand SaveCommand { get; }

        ObservableCollection<ProjectMemberViewModel> Members { get; }
    }
}
