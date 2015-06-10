using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class EmpProfessionalDetails
    {
        [Required(ErrorMessage = "Please enter company name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Please use alphabets and numeric only")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter designation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Please use alphabets and numeric only")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please enter first profile descritpion")]
        [Display(Name = "Profile Descritpion")]
        public string ProfileDescritpion { get; set; }

        //[Required(ErrorMessage = "Please select from date ")]
        [Display(Name = "From Date")]
        [DataType(DataType.Time)]
        public DateTime FromDate { get; set; }

        //[Required(ErrorMessage = "Please select to date")]
        [Display(Name = "To Date")]
        [DataType(DataType.Time)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}