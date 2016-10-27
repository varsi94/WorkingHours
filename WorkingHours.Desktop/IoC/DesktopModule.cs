using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkingHours.Client.IoC;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Services;
using WorkingHours.Desktop.ViewModel;

namespace WorkingHours.Desktop.IoC
{
    public class DesktopModule : ClientModule
    {
        public override void Load()
        {
            base.Load();
            LoadViewModels();
            LoadServices();
        }

        private void LoadServices()
        {
            Bind<IDialogService>().To<DialogService>().InTransientScope();
            Bind<ILoadingService>().ToMethod(x => (App)Application.Current);
        }

        private void LoadViewModels()
        {
            Bind<ILoginViewModel>().To<LoginViewModel>().InTransientScope();
            Bind<IMainViewModel>().To<MainViewModel>().InTransientScope();
            Bind<ISignUpViewModel>().To<SignUpViewModel>().InTransientScope();
            Bind<IChangePasswordViewModel>().To<ChangePasswordViewModel>().InTransientScope();
        }
    }
}
