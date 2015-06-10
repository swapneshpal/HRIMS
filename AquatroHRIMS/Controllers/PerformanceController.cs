using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.ActionFilters;
namespace AquatroHRIMS.Controllers
{
     [HRIMSActionFilter]
     [CustomException]
    public class PerformanceController : Controller
    {
        //
        // GET: /Performance/
        public ActionResult Index()
        {
            return View();
        }
	}
}