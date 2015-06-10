using AquatroHRIMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.ViewModel
{
    public class PersonalViewModel
    {

        public EmployeePersonalDetails objEmpPersonal { get; set; }
        public EmpEmeragancyContact objEmpEmergencyContact { get; set; }
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        //[System.Web.Mvc.Remote("CheckWorkEmail", "Employee", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Display(Name = "Work Email Id")]
        public string PersonalEmail { get; set; }


        [Required(ErrorMessage = "Please enter email name")]
        [Display(Name = "Personal Email Id")]
        //[Remote("CheckPersonalEmail", "Employee", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        public string WorkEmail { get; set; }

    }
}