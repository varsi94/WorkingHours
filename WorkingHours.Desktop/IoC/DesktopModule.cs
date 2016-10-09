using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.IoC;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.ViewModel;

namespace WorkingHours.Desktop.IoC
{
    public class DesktopModule : ClientModule
    {
        public override void Load()
        {
            base.Load();
            LoadViewModels();
        }

        private void LoadViewModels()
        {
            Bind<ILoginViewModel>().To<LoginViewModel>().InTransientScope();
            Bind<IMainViewModel>().To<MainViewModel>().InTransientScope();
        }
    }
}
