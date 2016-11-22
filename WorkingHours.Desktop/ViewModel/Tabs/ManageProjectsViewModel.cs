using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.ViewModel.Tabs
{
    class ManageProjectsViewModel : TabViewModelBase, IManageProjectsViewModel
    {
        ICommand SaveCommand { get; }
        UpdateProjectViewModel updateProjectViewModel;
        private readonly ILoadingService loadingService;
        private readonly IProjectManager projectManager;
        private readonly IDialogService dialogService;

        ManageProjectsViewModel(LoginInfo loginInfo, ILoadingService loadingService, IProjectManager projectManager, IDialogService dialogService) : base(loginInfo)
        {
            this.dialogService = dialogService;
            this.projectManager = projectManager;
            this.loadingService = loadingService;
            SaveCommand = new RelayCommand(ExecuteSaveCommand)
        }

        private  void ExecuteSaveCommand()
        {
            try
            {
                loadingService.ShowIndicator("Saving...");
                //await projectManager.UpdateAsync(updateProjectViewModel.MyProperty);
                //SelectedIssue.EndEdit();

                ReloadProject();
            }
            catch (ServerException)
            {
                dialogService.ShowError("Error during save!",
                    "There is a conflicting edit! Reload the issue, and update it again!");
            }
            finally
            {
                loadingService.HideIndicator();
            }
        }

        protected override Task OnProjectChanged()
        {
            updateProjectViewModel = new UpdateProjectViewModel();
            return base.OnProjectChanged();
        }

    }
}
