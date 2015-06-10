using AquatroHRIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.ViewModel
{
    public class EmployeeViewModel
    {

        [Required(ErrorMessage = "Please enter email id")]
        [System.Web.Mvc.Remote("CheckWorkEmail", "Employee", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Display(Name = "Work Email Id")]
        public string EmployeeEmailId { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        [Remote("CheckWorkEmailUpdate", "Employee", AdditionalFields = "hdnEmployeeID", ErrorMessage = "Email id already registered")]
        [Display(Name = "Work Email Id")]
        public string EmployeeEmailIdUpdate { get; set; }

        [Required(ErrorMessage = "Please enter email name")]
        [Display(Name = "Personal Email Id")]
        [Remote("CheckPersonalEmail", "Employee", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        public string PersonalEmail { get; set; }

        public string hdnEmployeeID { get; set; }

        [Required(ErrorMessage = "Please enter email name")]
        [Display(Name = "Personal Email Id")]
        [Remote("CheckPersoanlEmailUpdate", "Employee", AdditionalFields = "hdnEmployeeID", ErrorMessage = "Email id already registered")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email id")]
        public string PersonalEmailUpdate { get; set; }

        [Required(ErrorMessage = "Country Required.")]
        public int? Title { get; set; }

        public int RecordID { get; set; }


        public Employee Employee { get; set; }
        public IEnumerable<EmployeeList> EmpList{ get; set; }

        public EmployeePersonalDetails EmpPersonal { get; set; }

        public EmpProfessionalDetails EmpProfessional { get; set; }

        public EmpEducationDetails EmpEducation { get; set; }

        public Department Dept { get; set; }

        public Login Login { get; set; }

        public Designation Design { get; set; }

        public RollAccess Access { get; set; }

        public int RollAccessID { get; set; }

        [Display(Name = "Roll Name ")]
        public string RollName { get; set; }

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }

        public SelectList DesignationList { get; set; }

        public string[] SelectedDesignation { get; set; }

        public SelectList ReportList { get; set; }

        public string[] SelectedReportHead { get; set; }

        public SelectList DepartmentList { get; set; }

        public string[] SelectedDepartment { get; set;  }

        public SelectList RollAccessList { get; set; }

        public string[] SelectedRollAccess { get; set; }

        public SelectList TitleList{ get; set; }

        public string[] SelectedTitle { get; set; }

        public SelectList LocationList { get; set; }

        public string[] SelectedLocation { get; set; }

        [Display(Name = "Department Type")]
        public SelectList DepartTypeList { get; set; }
        public string[] SelectedDepartmentType { get; set; }


        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string DOJ { get; set; }
        public int iLocation { get; set; }
        public int iReportingHead { get; set; }
       
        public int DepID { get; set; }
        public int DepTypeID { get; set; }
        public int DesgID { get; set; }
    }

    public class ddlDesignation
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }

    public class ddlReportHead
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }

    public class ddlDepartment
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }

    public class ddlRollAccess
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlEmpTitle
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlLocation
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlDepartmentType
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
}
