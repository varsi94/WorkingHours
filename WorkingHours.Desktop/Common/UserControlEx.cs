using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorkingHours.Desktop.Common
{
    public class UserControlEx : UserControl
    {
        public UserControlEx()
        {
            this.IsVisibleChanged += OnIsVisibleChanged;
        }

        private async void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((Content as FrameworkElement)?.DataContext is ILoadAware)
            {
                bool visibility = (bool) e.NewValue;
                var vm = ((ILoadAware) (Content as FrameworkElement).DataContext);
                if (visibility)
                {
                    await vm.OnShown();
                }
                else
                {
                    await vm.OnHidden();
                }
            }
        }
    }
}
