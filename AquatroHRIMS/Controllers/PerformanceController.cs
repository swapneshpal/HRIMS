using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.ActionFilters;
using AquatroHRIMS.Models;
using HRIMS;
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
        //Added Swpnesh:-
        [HttpGet]
        public ActionResult AddQuadrants()
        {
            AddQuadrant quadrant = new AddQuadrant();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(quadrant);
        }
        [HttpPost]
        public JsonResult AddQuadrants(string QuadrantName)
        {
            try
            {
                cGoals goal = cGoals.Create();
                goal.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                goal.sName = QuadrantName.ToString();
                goal.bIsActive = true;
                goal.Save();
                ViewBag.DataSaved = "Goal added successfully";
                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
	}
}