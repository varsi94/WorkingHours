﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkingHours.WebClient.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}