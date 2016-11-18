using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Messaging;
using WorkingHours.Desktop.Interfaces.ViewModels;
using WorkingHours.Desktop.Common;

namespace WorkingHours.Desktop.ViewModel
{
    public class ChangePasswordViewModel : ValidatableViewModelBase, IChangePasswordViewModel
    {
        private string oldPassword;

        [Required(ErrorMessage = "Old password field is required!")]
        public string OldPassword
        {
            get { return oldPassword; }

            set { Set(ref oldPassword, value); }
        }


        private string newPassword;

        [Required(ErrorMessage = "Password field is required!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long!")]
        public string NewPassword
        {
            get { return newPassword; }

            set { Set(ref newPassword, value); }
        }


        private string newPasswordConfirmed;

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords must match each other!")]
        public string NewPasswordConfirmed
        {
            get { return newPasswordConfirmed; }

            set { Set(ref newPasswordConfirmed, value); }
        }

        protected override void FillInProperties()
        {
            PropertiesToValidate.Add(nameof(OldPassword), () => OldPassword);
            PropertiesToValidate.Add(nameof(NewPassword), () => NewPassword);
            PropertiesToValidate.Add(nameof(NewPasswordConfirmed), () => NewPasswordConfirmed);
        }
    }
}
