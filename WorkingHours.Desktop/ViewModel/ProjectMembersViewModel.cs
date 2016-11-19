using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Dialogs;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Model;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectMembersViewModel : TabViewModelBase, IProjectMembersViewModel
    {
        private readonly IUserManager userManager;

        public ICommand AddCommand { get; }

        public ICommand SearchCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand RemoveCommand { get; }

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
        

        private bool isReadonly;
        private readonly IDialogService dialogService;
        private readonly ILoadingService loadingService;

        public bool IsReadonly
        {
            get { return isReadonly; }

            set { Set(ref isReadonly, value); }
        }

        public ProjectMembersViewModel(LoginInfo loginInfo, IUserManager userManager, IProjectManager projectManager, IDialogService dialogService,
            ILoadingService loadingService) : base(loginInfo)
        {
            AddCommand = new RelayCommand<UserViewModel>(ExecuteAddCommand);
            SearchCommand = new RelayCommand<SearchEventArgs>(ExecuteSearchCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            RemoveCommand = new RelayCommand<ProjectMemberViewModel>(ExecuteRemoveCommand);
            this.userManager = userManager;
            this.dialogService = dialogService;
            this.projectManager = projectManager;
            this.loadingService = loadingService;
        }

        private async void ExecuteRemoveCommand(ProjectMemberViewModel obj)
        {
            try
            {
                if (obj.IsActive)
                {
                    loadingService.ShowIndicator("Deleting...");
                    await projectManager.RemoveMemberFromProjectAsync(CurrentProject.Id, obj.Id);
                }
                else
                {
                    loadingService.ShowIndicator("Adding...");
                    await projectManager.AddMembersToProjectAsync(CurrentProject.Id, new Dictionary<int, Roles>() { { obj.Id, obj.RoleInProject } });
                }
                ReloadProject();
            }
            catch (InvalidOperationException)
            {
                dialogService.ShowError("Error", "You can not delete yourself!");
            }
            finally
            {
                loadingService.HideIndicator();
            }
        }

        private async void ExecuteSaveCommand()
        {
            try
            {
                loadingService.ShowIndicator("Saving...");
                await
                    projectManager.AddMembersToProjectAsync(CurrentProject.Id,
                        members.Where(m => m.IsChanged).ToDictionary(m => m.Id, m => m.RoleInProject));
                ReloadProject();
            }
            catch (InvalidOperationException)
            {
                dialogService.ShowError("Error", "You can not change your own status!");
        }
            finally
            {
                loadingService.HideIndicator();
            }
        }

        protected override Task OnProjectChanged()
        {
            Members = new ObservableCollection<ProjectMemberViewModel>(CurrentProject.Members.Select(x => new ProjectMemberViewModel(x)));
            foreach (var member in Members)
            {
                member.IsChanged = false;
            }
            IsReadonly = projectManager.LoginInfo.Role == Roles.Employee;
            return base.OnProjectChanged();
        }

        private async void ExecuteAddCommand(UserViewModel obj)
        {
            if (obj == null)
            {
                return;
            }

            loadingService.ShowIndicator("Adding member...");
               await projectManager.AddMembersToProjectAsync(CurrentProject.Id, new Dictionary<int, Shared.Model.Roles>() { { obj.Id, Shared.Model.Roles.Employee } });
                ReloadProject();
            loadingService.HideIndicator();
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
