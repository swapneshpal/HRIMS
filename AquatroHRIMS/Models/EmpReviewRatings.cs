using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class EmpReviewRatings
    {
        public string sName { get; set; }
        public string sManagerComment { get; set; }
        public int objRatings { get; set; }
        public string sManagerSignature { get; set; }
        public string sEmployeeAcceptance { get; set; }
        public string sEmployeeSignature { get; set; }
        public int objEmpLogin { get; set; }
        public int iEmployeeID { get; set; }
    }
}