using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace AquatroHRIMS.Models
{
    public class EmpQuadrantMeasures
    {
        public string sName  {get; set;}
        public string sMeasures { get; set; }
        public int objDepartmentType { get; set; }
        public int objEmpLogin { get; set; }
        public int objGoals { get; set; }
        public bool bIsActive { get; set; }
    }
    public class AddQuadrant
    {
        public string QuadrantName { get; set; }

        public SelectList QuadrantDepartmentTypeList { get; set; }

        public string[] SelectedQuadrantDepartmentType { get; set; }
    }
    public class ddlQuadrantDepartmentType
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }
    
    
}