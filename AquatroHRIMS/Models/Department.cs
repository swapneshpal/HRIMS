using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class Department
    {
        [Display(Name = "Department")]
        public int DeptID { get; set; }

        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }
    }
}