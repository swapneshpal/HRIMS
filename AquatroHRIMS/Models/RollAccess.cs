using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class RollAccess
    {
        public int RollAccessID { get; set; }

        public string RollName { get; set; }

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
    }
}