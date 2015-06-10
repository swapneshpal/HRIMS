using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.Models
{
    public class Designation
    {
        [Required(ErrorMessage="Please select designation")]
        [Display(Name="Designation")]
        public int DesignationID { get; set; }

        public string Name { get; set; }

        public bool bIsActive { get; set; }

    }
   
   
    
}