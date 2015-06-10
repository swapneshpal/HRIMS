using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquatroHRIMS.Models;

namespace AquatroHRIMS.ViewModel
{
    public class DashBoardView
    {
        public IEnumerable<NewsList> objNews { get; set; }
        public IEnumerable<PendingLeaveList> objPendingLeave { get; set; }
        public IEnumerable<NewJoineeList> objNewJoinee { get; set; }
        public IEnumerable<AssignProject> objAssignProject{ get; set; }
        public IEnumerable<BirthDayList> objBirthDay { get; set; }

        public string DailyQuates { get; set; }
    }

    public class NewsList {
        public string Heading { get; set; }
        public string Description { get; set; }
    
    }
    public class PendingLeaveList
    {
        public string LeaveName { get; set; }
        public string Status { get; set; }

    }
    public class NewJoineeList
    {
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string DepartmentType { get; set; }
    }
    public class AssignProject
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
    }
    public class BirthDayList
    {
        public string Name { get; set; }
        public string DOB { get; set; }
    }
}