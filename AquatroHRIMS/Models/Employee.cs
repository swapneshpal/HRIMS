using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.Models
{
    public class Employee
    {
        [Display(Name = "Reporting Head")]
        public string ReportingHead { get; set; }

        [Required(ErrorMessage="Please select date of joining")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }

        [Display(Name = "Location")]
        public string  Location { get; set; }
    }
    public class EmployeeList
    {

        public string EmpID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string DepartmentType { get; set; }
        public string DOJ { get; set; }
        public string WorkEmail { get; set; }
    }
}