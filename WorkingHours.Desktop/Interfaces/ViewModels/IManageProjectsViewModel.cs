using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Desktop.ViewModel.Tabs;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IManageProjectsViewModel : IViewModel
    {
        ICommand SaveCommand { get; }

        ICommand DiscardChangesCommand { get; }

        UpdateProjectViewModel Project { get; }
    }
}
