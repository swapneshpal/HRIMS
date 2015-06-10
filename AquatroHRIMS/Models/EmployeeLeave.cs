using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.Models
{
    public class EmployeeLeave
    {

        public int iID { get; set; }

        public string Leavetype { get; set; }

        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

   
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        public int no_ofDay { get; set; }
        [Required(ErrorMessage="Please enter leave reason")]
        public string Reason { get; set; }

        public string LeaveStatus { get; set; }

        public bool IsActive { get; set; }

   }

}