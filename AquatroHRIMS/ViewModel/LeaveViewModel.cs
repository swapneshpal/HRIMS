using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.Models;
namespace AquatroHRIMS.ViewModel
{
    public class LeaveViewModel
    {
        public LeaveType objLeaveType { get; set; }
        public EmployeeLeave objEmployeeLeave { get; set; }
        public Employee objEmployee { get; set; }
        public SelectList LeaveTypeList { get; set; }
        public string[] SelectedLeaveType { get; set; }

        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDesignation { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeReportingHead { get; set; }

      

    }


    public class ddlLeaveType
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class PendingLeave
    {
        public string LeaveID { get; set; }
        public string EmpName { get; set; }
        public string Days { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveType { get; set; }
        public string LeaveStatus { get; set; }

    }
}