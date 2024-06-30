using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your email address!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [ViewData]
        public string StatusMessage { get; set; }
    }
}
