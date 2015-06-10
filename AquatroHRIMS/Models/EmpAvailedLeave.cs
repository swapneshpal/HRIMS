using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class EmpAvailedLeave
    {
        public string sName { get; set; }

        public int iRemainingLeave { get; set; }

        public int iConsumeLeave { get; set; }

        public bool bIsActive { get; set; }
    }
}