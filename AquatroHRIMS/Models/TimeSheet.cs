using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.Models
{
    public class TimeSheet
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TotalTime { get; set; }

        public string Mon { get; set; }

        public string Tue { get; set; }

        public string Wed { get; set; }

        public string Thu { get; set; }

        public string Fri { get; set; }

        public string Sat { get; set; }

        public string Sun { get; set; }


        public SelectList ProjectList { get; set; }

        public string[] SelectedProject { get; set; }

        public SelectList ActivityList { get; set; }

        public string[] SelectedActivity { get; set; }

        public SelectList EmpList { get; set; }

        public string[] SelectedEmp { get; set; }
    }

    public class ddlProjectTimeSheet
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class ddlActivity
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class ddlEmp
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public bool bIsActive { get; set; }

    }
    public class TimeReview
    {
        public string date { get; set; }

        public string sunday { get; set; }

        public string Total { get; set; }

        public string status { get; set; }
    }
    public class WeekDate
    {
        public DateTime sDate { get; set; }

        public DateTime eDate { get; set; }
    }
}