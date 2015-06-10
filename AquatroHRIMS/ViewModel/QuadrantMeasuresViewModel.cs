using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AquatroHRIMS.Models;

namespace AquatroHRIMS.ViewModel
{
    public class QuadrantMeasuresViewModel
    {
        public EmpQuadrantMeasures objEmpQudrants { get; set; }
        public EmpReviewRatings objReviewRating { get; set; }
        public SummaryComments objSummaryComment { get; set; }
        public CareerDevelopmentPlan objCareerDevelopmentPlan { get; set; }
        public int intGoalTitleId { get; set; }
        public SelectList DepartmentTypeModel { get; set; }
        public SelectList GoalTileModel { get; set; }
        public List<EmpList> empList = new List<EmpList>();
        public SelectList StatusModel { get; set; }
        public SelectList EmployeeModel { get; set; }
        public List<lstEmpQuadrantList> QuadList = new List<lstEmpQuadrantList>();
        public string[] GoalID { get; set; }
        public int[] goalStatusID { get; set; }
         [Required(ErrorMessage = "Please enter employee comments")]
        public string[] empComments { get; set; }
        public string[] ManagerComments { get; set; }
        public int[] EmpUniqueGoalID { get; set; }
        public SelectList RatingModel { get; set; }
        public string ManagerName { get; set; }
        public string EmployeeName { get; set; }
        public string MgrComment { get; set; }
        public int ratingId { get; set; }
        public string[] SummaryComments { get; set; }
        public List<lstQuadrantList> list1 = new List<lstQuadrantList>();       
        public List<lstDevelopmentGoalList> list2 = new List<lstDevelopmentGoalList>();
        public List<lstSummaryComments> list3 = new List<lstSummaryComments>();
        public string[] SelectedDeptList { get; set; }
        public int[] DevelopmentPlanID { get; set; }
        public int hdnEmployeeID { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string strErrorMsg { get; set; }
        public string[] EmployeeGoalID { get; set; }
        public string[] Measures { get; set; }
        public bool MangerFlag { get; set; }
        public bool EmpFlag { get; set; }
        public string EmpAcceptance { get; set; }
        public bool AdminFlag { get; set; }
        public bool ReportingHeadFlag { get; set; }
        public List<FileAttachment> listFileAttachment = new List<FileAttachment>();

    }
    public class ddlDepartmentTypeList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlGoalTitleList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class EmpList
    {
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string empID { get; set; }
    }
    public class ddlRatingsList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlEmployeeList1
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class lstEmpQuadrantList
    {
        public string GoalName { get; set; }
        public string Measures { get; set; }
        public int Count { get; set; }
        public int GoalId { get; set; }
        public int statusID { get; set; }
        public string StatusName { get; set; }
        public string empcomment { get; set; }
        public int EmpGoalID { get; set; }
        public string ManagerComment { get; set; }
        public bool ManagerFlag { get; set; }
    }
    public class ddlStatusList
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    public class ddlStatusList1
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }
    public class lstDevelopmentGoalList
    {
        public string GoalName { get; set; }
        public string ActionRequired { get; set; }
        public string Tracking { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
        public int ID { get; set; }
        public string ManagerComment { get; set; }
    }
    public class lstSummaryComments
    {
        public string SummaryComments { get; set; }        
    }
    public class FileAttachment
    {
        public string ID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
    public class QuadrantMeasureList
    {
        public string QuadrantID { get; set; }
        public string QuadrantName { get; set; }
        public string DepartmentName { get; set; }
    }

}