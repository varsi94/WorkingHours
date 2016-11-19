using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkingHours.WebClient.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old password is required!")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required!")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6 characters long!")]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Pasword confirmed is required!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords must match each other!")]
        [Display(Name = "Password confirmed")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirmed { get; set; }
    }
}