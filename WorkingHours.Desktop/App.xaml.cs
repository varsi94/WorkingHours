using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WorkingHours.Desktop.Interfaces.Services;
using WorkingHours.Desktop.Services;

namespace WorkingHours.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ILoadingService
    {
        private MainWindow Window => (MainWindow) MainWindow;

        public void HideIndicator()
        {
            Window.LoadingGrid.Visibility = Visibility.Collapsed;
        }

        public void ShowIndicator(string message)
        {
            Window.LoadingMessage.Text = message;
            Window.LoadingGrid.Visibility = Visibility.Visible;
        }
    }
}
