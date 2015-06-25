using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS;
using System.Data;
using AquatroHRIMS.Models;

namespace AquatroHRIMS.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        public ActionResult Menu()
        {

            DataTable dt = HRMS.Menu.MenuTable();
            var finalresult = from result in dt.AsEnumerable()
                              select new MenuItem
                              {
                                  id = result.Field<int>("id")
                                  ,
                                  name = result.Field<string>("MenuName")
                                  ,
                                  actionName = result.Field<string>("ActionName")
                                  ,
                                  controllerName = result.Field<string>("ControllerName")
                                  ,
                                  parantId = result.Field<int>("parantID")
                                  ,
                                  menuLevel = result.Field<int>("level")
                              };

            return PartialView("_Menu",finalresult);
        }
	}
}