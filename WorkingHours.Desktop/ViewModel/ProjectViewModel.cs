using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;
using WorkingHours.Client.Model;
using WorkingHours.Desktop.Common;
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

        public Roles RoleInProject
        {
            get { return roleInProject; }

            private set { Set(ref roleInProject, value); }
        }

        public ProjectViewModel(LoginInfo loginInfo, IProjectManager projectManager)
        {
            MessengerInstance.Register<NotificationMessage<ProjectHeader>>(this, MessageTokens.CurrentProjectChanged, CurrentProjectChanged);
            this.projectManager = projectManager;
            this.loginInfo = loginInfo;
        }

        private async void CurrentProjectChanged(NotificationMessage<ProjectHeader> obj)
        {
            if (obj.Content == null)
            {
                selectedProject = null;
            }
            else
            {
                selectedProject = await projectManager.GetProjectAsync(obj.Content.Id);
                RoleInProject = (Roles)Enum.Parse(typeof(Roles), selectedProject.Members.SingleOrDefault(x => x.Id == loginInfo.Id).Role);
            }
        }
    }
}
