using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Common;

namespace WorkingHours.Desktop.ViewModel
{
    public abstract class TabViewModelBase : LoadAwareViewModelBase
    {
        public override Task OnShown()
        {
            MessengerInstance.Send(new NotificationMessage(null), MessageTokens.ReloadProjectToken);
            return base.OnShown();
        }
    }
}
