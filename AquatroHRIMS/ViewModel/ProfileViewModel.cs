using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquatroHRIMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace AquatroHRIMS.ViewModel
{
    public class ProfileViewModel
    {
        public EmpEducationDetails objEmpEducation { get; set; }
        public EmpProfessionalDetails objEmpProfessional { get; set; }
        public EmployeePersonalDetails objEmpPersonal { get; set; }
    }
}