using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AquatroHRIMS.Models;
using GridMvc;
using System.Data;


namespace AquatroHRIMS.ViewModel
{
    public class EmployeeGoalsViewModel : EmpGoal
    {
        public EmpGoal objEmpGoals { get; set; }
       
        public SelectList StatusModel { get; set; }
        public SelectList GoalTitleModel { get; set; }
        public SelectList EmployeeModel { get; set; }       
        public string completion { get; set; }
        public string ID { get; set; }
        public string type { get; set; }
        public int recordID { get; set; }
        public int titleID { get; set; }
        public int statusID { get; set; }
        public string description { get; set; }
        public string[] SelectedEmployee { get; set; }       
        public string [] measures {get; set;}
        public string[][] QuadrantName { get; set; }
        public SelectList quadtants { get; set; }
        public string[] GoalID { get; set; }
        public int[] goalStatusID { get; set; }
        public string[] empComments { get; set; }
        public string[] ManagerComments { get; set; }
       


        public List<EmpList> empList { get; set; }
    }
    public class ddlGoaltitle
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlStatus
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlEmployeeList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class lstQuadrantList
    {
        public string  GoalName{ get; set; }
        public string Measures { get; set; }
        public int Count { get; set; }
        public int GoalId { get; set; }
        public string empcomment { get; set; }
        public int EmpGoalID { get; set; }
        public string ManagerComment { get; set; }
        public bool EmpFlag { get; set; }
        public bool ManagerFlag { get; set; }
              
    }
   
   
}