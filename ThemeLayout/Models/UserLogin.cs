using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThemeLayout.Models
{
    public class UserLogin
    {
        [DisplayName("Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required!")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required!")]
        public string Password { get; set; }

        [DisplayName("Forgot Password ?")]
        public bool RememberMe { get; set; }
    }
}