using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Desktop.Dialogs;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectMembersViewModel : TabViewModelBase, IProjectMembersViewModel
    {
        private readonly IUserManager userManager;

        public ICommand AddCommand { get; }

        public ICommand SearchCommand { get; }

        public ICommand SaveCommand { get; }
        private List<UserViewModel> searchResults;

        public List<UserViewModel> SearchResults
        {
            get { return searchResults; }
            protected set { Set(ref searchResults, value); }
        }

        private ObservableCollection<ProjectMemberViewModel> members;
        private readonly IProjectManager projectManager;

        public ObservableCollection<ProjectMemberViewModel> Members
        {
            get { return members; }
            protected set { Set(ref members, value); }
        }
        
        public ProjectMembersViewModel(IUserManager userManager, IProjectManager projectManager)
        {
            AddCommand = new RelayCommand<UserViewModel>(ExecuteAddCommand);
            SearchCommand = new RelayCommand<SearchEventArgs>(ExecuteSearchCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            this.userManager = userManager;
            this.projectManager = projectManager;
        }

        private async void ExecuteSaveCommand()
        {
            await projectManager.AddMembersToProjectAsync(CurrentProject.Id, members.ToDictionary(m => m.Id, m => m.RoleInProject));
        }

        protected override  Task OnProjectChanged()
        {
            Members = new ObservableCollection<ProjectMemberViewModel>(CurrentProject.Members.Select(x => new ProjectMemberViewModel(x)));
            return base.OnProjectChanged();
        }
        private async void ExecuteAddCommand(UserViewModel obj)
        {
            if (obj == null)
            {
                //DialogWrapper<> d = new DialogWrapper<>();
            }
            else
            {
               await projectManager.AddMembersToProjectAsync(CurrentProject.Id, new Dictionary<int, Shared.Model.Roles>() { { obj.Id, Shared.Model.Roles.Employee } });
                ReloadProject();
            }
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
