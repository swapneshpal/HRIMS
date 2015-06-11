using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.Models
{
    public class ExternalProjectHead
    {
        [Required(ErrorMessage="Please enter external head name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="Please enter alphabets only")]
        public string ProjectHeadName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter alphabets only")]
        [Required(ErrorMessage = "Please enter external head name")]
        public string ContactNo { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter alphabets only")]
        [Required(ErrorMessage = "Please enter external head name")]
        public string EmailID { get; set; }
     
    }
}