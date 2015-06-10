using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace AquatroHRIMS.Models
{
    public class Login
    {
         [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Required(ErrorMessage="Please enter email id")]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

  

        public int objRollAccess { get; set; }
        public string FirstTime { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Please enter email id")]
        [System.Web.Mvc.Remote("CheckEmailValid", "Login", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        public string ForgotEmailID { get; set; }
        public bool RememberMe { get; set; }
    }
    public class ChangePassword
    {
        public string hdnEmail { get; set; }
       
        [Required(ErrorMessage = "Please enter current password")]
        [System.Web.Mvc.Remote("CheckOldPassword", "Login", ErrorMessage = "Invalid old password")]
        public string CurrentPassword { get; set; }

        [StringLength(30, MinimumLength = 6, ErrorMessage = "Please enter 6 digit password")]
        [Required(ErrorMessage = "Please enter new password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}