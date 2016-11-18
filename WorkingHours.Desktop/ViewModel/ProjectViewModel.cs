using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Messaging;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectViewModel : ViewModelBase, IProjectViewModel
    {
        private readonly IProjectManager projectManager;
        private ProjectInfo selectedProject;
        private readonly LoginInfo loginInfo;
        
        private Roles roleInProject;
        private readonly ILoadingService loadingService;

        public Roles RoleInProject
        {
            get { return roleInProject; }

            private set { Set(ref roleInProject, value); }
        }

        public ProjectViewModel(LoginInfo loginInfo, IProjectManager projectManager, ILoadingService loadingService)
        {
            MessengerInstance.Register<NotificationMessage<ProjectHeader>>(this, MessageTokens.CurrentProjectChanged, CurrentProjectChanged);
            MessengerInstance.Register<NotificationMessage>(this, MessageTokens.ReloadProjectToken, ReloadProject);
            this.projectManager = projectManager;
            this.loginInfo = loginInfo;
            RoleInProject = Roles.Employee;
            this.loadingService = loadingService;
        }

        private async void ReloadProject(NotificationMessage obj)
        {
            if (selectedProject == null)
            {
                return;
            }

            await LoadProjectAsync(selectedProject.Id);
        }

        private async Task LoadProjectAsync(int id)
        {
            loadingService.ShowIndicator("Loading project...");
            selectedProject = await projectManager.GetProjectAsync(id);
            loadingService.HideIndicator();
            MessengerInstance.Send(new NotificationMessage<ProjectInfo>(selectedProject, null),
                MessageTokens.ProjectLoadedToken);
        }

        private async void CurrentProjectChanged(NotificationMessage<ProjectHeader> obj)
        {
            if (obj.Content == null)
            {
                selectedProject = null;
            }
            else
            {
                await LoadProjectAsync(obj.Content.Id);
                RoleInProject = selectedProject.Members.SingleOrDefault(x => x.Id == loginInfo.Id).RoleInProjectEnum;
            }
        }
    }
}
