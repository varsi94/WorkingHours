using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.Interfaces;
using WorkingHours.Desktop.Messaging;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Shared.Dto;
using WorkingHours.Desktop.Common;

namespace WorkingHours.Desktop.ViewModel
{
    public class NewProjectDialogViewModel : ValidatableViewModelBase, INewProjectViewModel
    {
        private DateTime? deadline;

        public DateTime? Deadline
        {
            get { return deadline; }

            set { Set(ref deadline, value); }
        }

        private string name;

        [Required(ErrorMessage = "Name field is required!")]
        public string Name
        {
            get { return name; }

            set { Set(ref name, value); }
        }


        protected override void FillInProperties()
        {
            PropertiesToValidate.Add(nameof(Name), () => Name);
        }
    }
}
