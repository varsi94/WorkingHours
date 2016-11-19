using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Common;
using WorkingHours.Desktop.Messaging;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.ViewModel
{
    public abstract class TabViewModelBase : LoadAwareViewModelBase
    {
        protected ProjectInfo CurrentProject { get; private set; }

        public TabViewModelBase()
        {
            MessengerInstance.Register<NotificationMessage<ProjectInfo>>(this, MessageTokens.ProjectLoadedToken, ProjectLoaded);
        }

        private async void ProjectLoaded(NotificationMessage<ProjectInfo> obj)
        {
            CurrentProject = obj.Content;
            await OnProjectChanged();
        }

        public override Task OnShown()
        {
            var msg = new NotificationMessage(null);
            MessengerInstance.Send(msg, MessageTokens.ReloadProjectToken);
            return base.OnShown();
        }

        protected virtual Task OnProjectChanged()
        {
            return Task.FromResult<object>(null);
        }

        protected void ReloadProject()
        {
            var msg = new NotificationMessage(null);
            MessengerInstance.Send(msg, MessageTokens.ReloadProjectToken);
        }
    }
}
