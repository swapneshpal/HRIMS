using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.Models
{
    public class LeaveType
    {
        [Display(Name="Leave Type")]
        public string LeaveName { get; set; }
        public int MaxAllowDays { get; set; }
        public bool IsActive { get; set; }
    }
}