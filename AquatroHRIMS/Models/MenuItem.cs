using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquatroHRIMS.Models
{
    public class MenuItem
    {
        public int id;
        public int parantId;
        public string name;
        public string actionName;
        public string controllerName;
        public int menuLevel;
    }
}