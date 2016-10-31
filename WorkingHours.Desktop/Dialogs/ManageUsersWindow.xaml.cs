using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkingHours.Desktop.Interfaces;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.Dialogs
{
    /// <summary>
    /// Interaction logic for ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window, ICloseable
    {
        public ManageUsersWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ILoadAwareViewModel)
            {
                await ((ILoadAwareViewModel) DataContext).OnLoaded();
            }

            if (DataContext is IManageUsersViewModel)
            {
                ((IManageUsersViewModel)DataContext).Window = this;
            }
        }
    }
}
