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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for ProjectControl.xaml
    /// </summary>
    public partial class ProjectControl : UserControl
    {
        private IProjectViewModel ViewModel => (IProjectViewModel)LayoutRoot.DataContext;

        public ProjectControl()
        {
            InitializeComponent();
            ViewModel.OnProjectChanged += ProjectChanged;
        }

        private void ProjectChanged()
        {
            TabControl.SelectedIndex = 0;
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                TabControl.SelectedIndex = 0;
            }
        }
    }
}
