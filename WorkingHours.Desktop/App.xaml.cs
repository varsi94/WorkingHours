using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            if (e.Exception is HttpRequestException)
            {
                MessageBox.Show("The server seems to be unreachable! Please try again later!", "Error during reaching the server");
            }
            else
            {
                MessageBox.Show("Unexpected error occured!", "Unexpected error");
            }
            HideIndicator();
        }

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
