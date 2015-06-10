using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class LeaveType
    {
        public string LeaveName { get; set; }
        public int MaxAllowDays { get; set; }
        public bool IsActive { get; set; }
    }
}