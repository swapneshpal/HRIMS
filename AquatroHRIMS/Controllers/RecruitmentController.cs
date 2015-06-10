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
    public class RecruitmentController : Controller
    {
        //
        // GET: /Recruitment/
        public ActionResult Index()
        {
            return View();
        }
	}
}