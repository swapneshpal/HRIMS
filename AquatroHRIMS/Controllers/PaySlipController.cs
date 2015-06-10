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
    public class PaySlipController : Controller
    {
        //
        // GET: /PaySlip/
        public ActionResult Index()
        {
            return View();
        }
	}
}