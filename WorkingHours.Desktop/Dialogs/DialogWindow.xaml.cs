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

namespace WorkingHours.Desktop.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private bool clicked = false;    
        public IDialogWrapper DialogWrapper { get; set; }

        public DialogWindow()
        {
            InitializeComponent();
        }

        private void OkClicked(object sender, EventArgs args)
        {
            clicked = true;
            DialogWrapper.Ok((content.Content as ContentControl)?.DataContext);
            Close();
            clicked = false;
        }

        private void CancelClicked(object sender, EventArgs args)
        {
            clicked = true;
            DialogWrapper.Cancel();
            Close();
            clicked = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (!clicked)
            {
                DialogWrapper.Cancel();
            }
        }
    }
}
