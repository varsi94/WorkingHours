using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkingHours.Desktop.Interfaces.Services;

namespace WorkingHours.Desktop.Services
{
    public class LoadingService : ILoadingService
    {
        public void HideIndicator()
        {
            App.Current.Window.LoadingGrid.Visibility = Visibility.Collapsed;
        }

        public void ShowIndicator(string message)
        {
            App.Current.Window.LoadingMessage.Text = message;
            App.Current.Window.LoadingGrid.Visibility = Visibility.Visible;
        }
    }
}
