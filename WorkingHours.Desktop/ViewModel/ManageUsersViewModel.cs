using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using GalaSoft.MvvmLight.CommandWpf;
using WorkingHours.Desktop.Interfaces;
using WorkingHours.Desktop.Model;
using WorkingHours.Desktop.Interfaces.Services;

namespace WorkingHours.Desktop.ViewModel
{
    public class ManageUsersViewModel : ViewModelBase, IManageUsersViewModel
    {
        private const int pageSize = 10;
        private int pageIndex;
        private readonly IUserManager userManager;
        private readonly IDialogService dialogService;
        private bool changedFromVM = false;

        public ICommand ChangeRoleCommand { get; }
        
        private string keyword;

        public string Keyword
        {
            get { return keyword; }

            set { Set(ref keyword, value); }
        }

        private int pageCount;

        public int PageCount
        {
            get { return pageCount; }

            set { Set(ref pageCount, value); }
        }

        public ICloseable Window { get; set; }
        
        private Dictionary<int, Roles> rolesToUpdateDict;

        public ICommand SaveChangesCommand { get; }
        
        private ObservableCollection<UserViewModel> usersShown;

        public ObservableCollection<UserViewModel> UsersShown
        {
            get { return usersShown; }

            set { Set(ref usersShown, value); }
        }

        public ICommand SearchCommand { get; }

        public ICommand NextCommand { get; }

        public ICommand PreviousCommand { get; }

        public ICommand CancelCommand { get; }

        public ManageUsersViewModel(IUserManager userManager, IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.userManager = userManager;
            NextCommand = new RelayCommand(ExecuteNextCommand, () => PageCount > pageIndex);
            PreviousCommand = new RelayCommand(ExecutePreviousCommand, () => pageIndex > 1);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            SaveChangesCommand = new RelayCommand(ExecuteSaveChangesCommand, () => rolesToUpdateDict.Count != 0);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            ChangeRoleCommand = new RelayCommand<RoleChangedModel>(ExecuteChangeRoleCommand);

            rolesToUpdateDict = new Dictionary<int, Roles>();
        }

        private void ExecuteChangeRoleCommand(RoleChangedModel obj)
        {
            if (changedFromVM)
            {
                return;
            }

            changedFromVM = true;
            bool isAny = rolesToUpdateDict.Any(x => x.Key == obj.User.Id);
            if (isAny)
            {
                var item = rolesToUpdateDict.Single(x => x.Key == obj.User.Id);
                UsersShown.Single(x => x.Id == obj.User.Id).Role = obj.NewRole;
                rolesToUpdateDict.Remove(item.Key);
                obj.User.IsChanged = !obj.User.IsChanged;
                obj.User.Role = obj.NewRole;
            }
            else
            {
                var newRole = obj.NewRole;
                var currentRole = UsersShown.Single(x => x.Id == obj.User.Id).Role;
                if (currentRole != newRole)
                {
                    rolesToUpdateDict.Add(obj.User.Id, newRole);
                    obj.User.IsChanged = !obj.User.IsChanged;
                    obj.User.Role = obj.NewRole;
                }
            }
            changedFromVM = false;
        }

        private void ExecuteCancelCommand()
        {
            Window.Close();
        }

        private async void ExecuteSaveChangesCommand()
        {
            try
            {
                await userManager.UpdateRolesAsync(rolesToUpdateDict);
                Window.Close();
            }
            catch (InvalidOperationException e)
            {
                dialogService.ShowError("Error occured", e.Message);
            }
        }

        private async void ExecuteSearchCommand()
        {
            rolesToUpdateDict.Clear();
            await UpdateUserList(Keyword, 1);
        }

        private async void ExecutePreviousCommand()
        {
            await UpdateUserList(Keyword, pageIndex - 1);
        }

        private async Task UpdateUserList(string keyword, int page)
        {
            var users = await userManager.ListUsersAsync(pageSize, page, keyword);
            UsersShown = new ObservableCollection<UserViewModel>(users.Items.Select(x => new UserViewModel(x)));
            PageCount = users.PageCount;
            pageIndex = users.PageIndex;
            (NextCommand as RelayCommand).RaiseCanExecuteChanged();
            (PreviousCommand as RelayCommand).RaiseCanExecuteChanged();

            foreach (var user in UsersShown.Where(x => rolesToUpdateDict.Any(y => y.Key == x.Id)))
            {
                user.IsChanged = true;
                user.Role = rolesToUpdateDict.Single(x => x.Key == user.Id).Value;
            }
        }

        private async void ExecuteNextCommand()
        {
            await UpdateUserList(Keyword, pageIndex + 1);
        }

        public async Task OnLoaded()
        {
            await UpdateUserList(string.Empty, 1);
        }
    }
}
