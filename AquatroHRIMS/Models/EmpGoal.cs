using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AquatroHRIMS.Models
{
    public class EmpGoal
    {        
        public string GoalName { get; set; }
        public string Descritpion { get; set; }
        public string ManagerDescription { get; set; }
        [Display(Name="Goal")]
        public int objGoals { get; set; }
        [Display(Name = "Status")]
        public int objStatus { get; set; }
        public int objEmpLogin { get; set; }
        public string sMeasures { get; set; }

    }
}