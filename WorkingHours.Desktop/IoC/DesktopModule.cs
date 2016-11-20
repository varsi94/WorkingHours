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
            Bind<IFileService>().To<FileService>().InTransientScope();
        }

        private void LoadViewModels()
        {
            Bind<ILoginViewModel>().To<LoginViewModel>().InTransientScope();
            Bind<IMainViewModel>().To<MainViewModel>().InTransientScope();
            Bind<ISignUpViewModel>().To<SignUpViewModel>().InTransientScope();
            Bind<IChangePasswordViewModel>().To<ChangePasswordViewModel>().InTransientScope();
            Bind<IProjectViewModel>().To<ProjectViewModel>().InTransientScope();
            Bind<IManageUsersViewModel>().To<ManageUsersViewModel>().InTransientScope();
            Bind<INewProjectViewModel>().To<NewProjectDialogViewModel>().InTransientScope();
            Bind<IManageIssuesViewModel>().To<ManageIssuesViewModel>().InTransientScope();
            Bind<IProjectMembersViewModel>().To<ProjectMembersViewModel>().InTransientScope();
            Bind<IWorkTimesViewModel>().To<WorkTimesViewModel>().InTransientScope();
            Bind<IReportIntervalViewModel>().To<ReportIntervalViewModel>().InTransientScope();
        }
    }
}
