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
    public partial class App : Application
    {
        public MainWindow Window => (MainWindow) MainWindow;

        public static new App Current => (App) Application.Current;

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

        private void HideIndicator()
        {
            Window.LoadingGrid.Visibility = Visibility.Collapsed;
        }
    }
}
