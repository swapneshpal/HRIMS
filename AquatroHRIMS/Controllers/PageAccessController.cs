using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.Controllers
{
    public class PageAccessController : Controller
    {
        //
        // GET: /PageAccess/
        public ActionResult CommingSoon()
        {
            return View();
        }
        public ActionResult Denied()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
	}
}