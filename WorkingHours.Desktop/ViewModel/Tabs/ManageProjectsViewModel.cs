using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingHours.Client.Exceptions;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.ViewModel.Tabs
{
    public class ManageProjectsViewModel : TabViewModelBase, IManageProjectsViewModel
    {
        public ICommand SaveCommand { get; }

        public ICommand DiscardChangesCommand { get; set; }
        
        private UpdateProjectViewModel project;

        public UpdateProjectViewModel Project
        {
            get { return project; }

            protected set { Set(ref project, value); }
        }

        private readonly ILoadingService loadingService;
        private readonly IProjectManager projectManager;
        private readonly IDialogService dialogService;

        public ManageProjectsViewModel(LoginInfo loginInfo, ILoadingService loadingService, IProjectManager projectManager, IDialogService dialogService) : base(loginInfo)
        {
            this.dialogService = dialogService;
            this.projectManager = projectManager;
            this.loadingService = loadingService;
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            DiscardChangesCommand = new RelayCommand(() => UpdateVM());
        }

        private void UpdateVM()
        {
            Project = new UpdateProjectViewModel
            {
                Name = CurrentProject.Name,
                Deadline = CurrentProject.Deadline,
                IsClosed = CurrentProject.IsClosed,
                IsManagerActiveInProject = CurrentProject.IsWriteable
            };
        }

        private async void ExecuteSaveCommand()
        {
            try
            {
                loadingService.ShowIndicator("Saving...");
                await
                    projectManager.UpdateAsync(new ProjectHeader
                    {
                        Name = Project.Name,
                        IsClosed = Project.IsClosed,
                        Deadline = Project.Deadline,
                        RowVersion = CurrentProject.RowVersion,
                        Id = CurrentProject.Id
                    });
            }
            catch (ServerException)
            {
                dialogService.ShowError("Error during save!",
                    "There is a conflicting edit! Reload the issue, and update it again!");
            }
            finally
            {
                loadingService.HideIndicator();
                ReloadProject();
            }
        }

        protected override Task OnProjectChanged()
        {
            UpdateVM();
            return base.OnProjectChanged();
        }
    }
}
