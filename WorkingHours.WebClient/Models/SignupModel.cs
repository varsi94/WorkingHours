using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkingHours.WebClient.Models
{
    public class SignupModel
    {
        [Required(ErrorMessage = "User name is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Full name is required!")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must match each other!")]
        [Display(Name = "Password confirmed")]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email address must be valid!")]
        [Required(ErrorMessage = "Email address is required!")]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }
    }
}