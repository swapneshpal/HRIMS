using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.Models
{
    public class Project
    {
        public int iID { get; set; }
        [Required(ErrorMessage = "Please enter project name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Please enter alphabets or numbers only")]
        [Display(Name="Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Client")]
        public int ClientID { get; set; }

        [Display(Name = "External Head")]
        public int ExternalHead { get; set; }

        [Display(Name = "Internal Head")]
        public int InternalHead { get; set; }

         [Required(ErrorMessage = "Please enter project description")]
        public string Description { get; set; }

        public string Complition { get; set; }

        [Required(ErrorMessage = "Please select start date")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select end date")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
          [Display(Name = "Status")]
        public int status { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProjectList
    {

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string InternalHead { get; set; }
        public string ExternalHead { get; set; }
        public string Status{ get; set; }
    }

   
}