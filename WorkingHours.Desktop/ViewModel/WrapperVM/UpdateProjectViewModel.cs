using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.ViewModel.Tabs
{
    public class UpdateProjectViewModel : NewProjectDialogViewModel
    {
        private bool isClosed;

        public bool IsClosed
        {
            get { return isClosed; }

            set { Set(ref isClosed, value); }
        }


        private bool isManagerActiveInProject;

        public bool IsManagerActiveInProject
        {
            get { return isManagerActiveInProject; }

            set { Set(ref isManagerActiveInProject, value); }
        }
    }
}
