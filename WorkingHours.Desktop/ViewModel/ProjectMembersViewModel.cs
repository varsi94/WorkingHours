using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectMembersViewModel : TabViewModelBase, IProjectMembersViewModel
    {
        private readonly IUserManager userManager;

        public ICommand AddCommand { get; }

        public ICommand SearchCommand { get; }

        private List<UserViewModel> searchResults;

        public List<UserViewModel> SearchResults
        {
            get { return searchResults; }
            protected set { Set(ref searchResults, value); }
        }

        public ProjectMembersViewModel(IUserManager userManager)
        {
            AddCommand = new RelayCommand<UserViewModel>(ExecuteAddCommand);
            SearchCommand = new RelayCommand<SearchEventArgs>(ExecuteSearchCommand);
            this.userManager = userManager;
        }

        private void ExecuteAddCommand(UserViewModel obj)
        {
        }

        private async void ExecuteSearchCommand(SearchEventArgs args)
        {
            SearchResults = null;
            var result = await userManager.ListUsersAsync(100, 1, args.Keyword);
            SearchResults = new List<UserViewModel>(result.Items.Select(x => new UserViewModel(x)));
            args.Populatable.PopulationComplete();
        }

        public override Task OnShown()
        {
            return base.OnShown();
        }
    }
}
