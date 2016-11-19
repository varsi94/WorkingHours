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
using WorkingHours.Desktop.Common;
using WorkingHours.Desktop.Interfaces;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Model;

namespace WorkingHours.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for ProjectMembersControl.xaml
    /// </summary>
    public partial class ProjectMembersControl : UserControlEx, IPopulatable
    {
        private IProjectMembersViewModel ViewModel
        {
            get { return (IProjectMembersViewModel) (this.Content as FrameworkElement).DataContext; }
        }

        public ProjectMembersControl()
        {
            InitializeComponent();
        }

        private void UserNameBox_OnPopulating(object sender, PopulatingEventArgs e)
        {
            e.Cancel = true;
            ViewModel.SearchCommand.Execute(new SearchEventArgs {Keyword = userNameBox.SearchText, Populatable = this});
        }

        public void PopulationComplete()
        {
            userNameBox.PopulateComplete();
        }
    }
}
