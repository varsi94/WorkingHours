using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Desktop.ViewModel;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IManageUsersViewModel : ILoadAwareViewModel
    {
        ICloseable Window { get; set; }

        ICommand SaveChangesCommand { get; }

        ICommand ChangeRoleCommand { get; }

        ICommand SearchCommand { get; }

        ICommand NextCommand { get; }

        ICommand PreviousCommand { get; }

        ICommand CancelCommand { get; }

        ObservableCollection<UserViewModel> UsersShown { get; set; }

        string Keyword { get; set; }

        int PageCount { get; set; }
    }
}
