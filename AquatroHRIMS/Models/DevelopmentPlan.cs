using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class DevelopmentPlan
    {
        public string sDevGoalName { get; set; }
        public string sActionRequired { get; set; }
        public string sTracking { get; set; }
        public string sManagerComment { get; set; }
        public int objEmpLogin { get; set; }
        public int objStatus { get; set; }
    }
}