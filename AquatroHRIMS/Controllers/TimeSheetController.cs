using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.Models;
using System.Globalization;
using HRIMS;
using AquatroHRIMS.ActionFilters;

namespace AquatroHRIMS.Controllers
{
    [CustomException]
    [HRIMSActionFilter]
    public class TimeSheetController : Controller
    {
        //
        // GET: /TimeSheet/
     static  DateTime dtCurrent = DateTime.Now;
       
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult PrevDate()
        {
            try
            {
                List<string> dtList = new List<string>();
                DateTime mon;
                dtCurrent = dtCurrent.AddDays(-7);
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
                DayOfWeek today = DateTime.Now.DayOfWeek;
                DateTime sow = dtCurrent.AddDays(-(today - fdow)).Date;
                mon = sow.AddDays(1);
                DateTime nd = mon;
                string sMonday = mon.ToString("dd/MM/yyyy");
                string sSunday = mon.AddDays(6).ToString("dd/MM/yyyy");
                dtList.Add(sMonday);
                dtList.Add(sSunday);
                dtList.Add(sow.AddDays(2).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(3).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(4).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(5).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(6).ToString("dd/MM/yyyy"));
                string str = Convert.ToString(TimeCount(nd));
                dtList.Add(str);
                return Json(dtList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public JsonResult NextDate()
        {
            try
            {
                List<string> dtList = new List<string>();
                DateTime mon;
                dtCurrent = dtCurrent.AddDays(7);
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
                DayOfWeek today = DateTime.Now.DayOfWeek;
                DateTime sow = dtCurrent.AddDays(-(today - fdow)).Date;
                mon = sow.AddDays(1);
                DateTime nd = mon;
                string sMonday = mon.ToString("dd/MM/yyyy");
                string sSunday = mon.AddDays(6).ToString("dd/MM/yyyy");
                dtList.Add(sMonday);
                dtList.Add(sSunday);
                dtList.Add(sow.AddDays(2).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(3).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(4).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(5).ToString("dd/MM/yyyy"));
                dtList.Add(sow.AddDays(6).ToString("dd/MM/yyyy"));
                string str = Convert.ToString(TimeCount(nd));
                dtList.Add(str);
                return Json(dtList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult MyTimeSheet()
        {
            try
            {
                DateTime mon;
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
                DayOfWeek today = DateTime.Now.DayOfWeek;
                DateTime sow = dtCurrent.AddDays(-(today - fdow)).Date;
                mon = sow.AddDays(1);

                TimeSheet timeSheet = new TimeSheet();
                timeSheet.StartDate = mon;
                timeSheet.EndDate = mon.AddDays(6);
                string workHours = TimeCount(mon);
                var dot = workHours.Contains('.');
               long  mm = 0;
                long  hh = 0;
                if(dot)
                {
                    string[] str = workHours.Split('.');
                    string str1 = str[0];
                    string str2 = str[1];
                    hh=Convert.ToInt64(str1);
                    
                    if(str2.Length>1)
                    {
                        string st = str2.Substring(0, 2);
                        mm = Convert.ToInt64(str2.Substring(0,2));
                        if(mm>60)
                        {
                            hh = hh + 1;
                            mm = mm - 60;
                        }
                    }
                    else
                    {
                        mm = Convert.ToInt64(str2);
                    }
                    workHours =hh+" : "+mm;
                }
                else
                {
                     workHours=workHours+ " : 00";
                }
               
                timeSheet.TotalTime = workHours;
                timeSheet.ProjectList = getProjectList();
                timeSheet.ActivityList = getActivityList();

                return View(timeSheet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string TimeCount(DateTime monday)
        {
            try
            {
                DateTime sunday = monday.AddDays(6);
                // DateTime stDate = DateTime.ParseExact(sunday.ToString(), "MM/dd/yyyy", null);
                DateTime enDate = DateTime.ParseExact(sunday.ToString().Trim(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                // DateTime stDate = DateTime.ParseExact(sunday.ToString(), "yyyy-MM-dd HH:mm:ss", null); 
                // DateTime enDate = DateTime.ParseExact(monday.ToString(), "MM/dd/yyyy", null);
                DateTime stDate = DateTime.ParseExact(monday.ToString().Trim(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                string id = HttpContext.User.Identity.Name;
                double count = 0;
                double Minut = 0;
                List<cTimesheet> objTimeSheet = cTimesheet.Find(" objEmpLogin = " + id + " and dtStartDate = " + stDate + " and dtEndDate = " + enDate);
                if (objTimeSheet.Count > 0)
                {
                    List<cTimeSheetActivity> objTimeSheetActivity = cTimeSheetActivity.Find(" objTimesheet = " + objTimeSheet[0].iID);
                    if (objTimeSheetActivity.Count > 0)
                    {
                        foreach (var item in objTimeSheetActivity)
                        {
                            List<cTimeSheetActivityDes> objDesc = cTimeSheetActivityDes.Find(" objTimeSheetActivity = " + item.iID);
                            if (objDesc.Count > 0)
                            {
                                foreach (var items in objDesc)
                                {
                                    if (items.sMonday != "")
                                    {
                                        string strMon = items.sMonday;
                                        string[] monT = strMon.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sTuesday != "")
                                    {
                                        string strTue = items.sTuesday;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sWed != "")
                                    {
                                        string strTue = items.sWed;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sThu != "")
                                    {
                                        string strTue = items.sThu;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sFri != "")
                                    {
                                        string strTue = items.sFri;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sSat != "")
                                    {
                                        string strTue = items.sSat;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sSun != "")
                                    {
                                        string strTue = items.sSun;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }

                                }
                            }

                        }
                    }


                }
                count = Minut / 60;

                return Convert.ToString(count);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        // GET: /TimeSheet/

        [HttpPost]
        public JsonResult MyTimeSheet(string startDate, string endDate,  string projectId, string ActivityID, string monday, string tuesday, string wed, string thu, string fri, string sat, string sun, string desc)
        {
            try
            {
                string id = HttpContext.User.Identity.Name;
                // DateTime dt = Convert.ToDateTime(startDate);
                DateTime stDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
                DateTime enDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
                List<cTimesheet> aObjTimesheet = cTimesheet.Find(" objEmpLogin = " + id + " and dtStartDate = " + stDate + " and dtEndDate = " + enDate);
                //List<cTimesheet> aObjTimesheet = cTimesheet.Find(" dtStartDate = " + stDate);
                if (aObjTimesheet.Count > 0)
                {
                    List<cTimeSheetActivity> aobjTimeActivity = cTimeSheetActivity.Find(" objTimesheet = " + aObjTimesheet[0].iID + " and objActivity = " + Convert.ToInt32(ActivityID));

                    if (aobjTimeActivity.Count > 0)
                    {

                        cTimeSheetActivityDes objDesc = cTimeSheetActivityDes.Create();
                        objDesc.objTimeSheetActivity.iObjectID = aobjTimeActivity[0].iID;
                        objDesc.sMonday = monday;
                        objDesc.sTuesday = tuesday;
                        objDesc.sWed = wed;
                        objDesc.sThu = thu;
                        objDesc.sFri = fri;
                        objDesc.sSat = sat;
                        objDesc.sSun = sun;
                        objDesc.sDescription = desc;
                        objDesc.bIsActive = true;
                        objDesc.Save();
                    }
                    else
                    {
                        cTimeSheetActivity objActive = cTimeSheetActivity.Create();
                        objActive.objActivity.iObjectID = Convert.ToInt32(ActivityID);
                        objActive.objTimesheet.iObjectID = aObjTimesheet[0].iID;
                        objActive.bIsActive = true;

                        objActive.Save();

                        cTimeSheetActivityDes objDesc = cTimeSheetActivityDes.Create();
                        objDesc.objTimeSheetActivity.iObjectID = objActive.iID;
                        objDesc.sMonday = monday;
                        objDesc.sTuesday = tuesday;
                        objDesc.sWed = wed;
                        objDesc.sTuesday = thu;
                        objDesc.sFri = fri;
                        objDesc.sSat = sat;
                        objDesc.sSun = sun;
                        objDesc.bIsActive = true;
                        objDesc.sDescription = desc;

                        objDesc.Save();
                    }

                }
                else
                {

                    cTimesheet objTimesheet = cTimesheet.Create();
                    objTimesheet.objEmpLogin.iObjectID = Convert.ToInt32(id);
                    objTimesheet.dtStartDate = stDate;
                    objTimesheet.dtEndDate = enDate;
                    objTimesheet.objProject.iObjectID = Convert.ToInt32(projectId);
                    objTimesheet.bIsActive = true;
                    objTimesheet.sTotalTime = "";
                    objTimesheet.Save();

                    cTimeSheetActivity objTimeActivity = cTimeSheetActivity.Create();
                    objTimeActivity.objActivity.iObjectID = Convert.ToInt32(ActivityID);
                    objTimeActivity.objTimesheet.iObjectID = Convert.ToInt32(objTimesheet.iID);
                    objTimesheet.bIsActive = true;
                    objTimeActivity.Save();

                    cTimeSheetActivityDes objTimeActDesc = cTimeSheetActivityDes.Create();
                    objTimeActDesc.objTimeSheetActivity.iObjectID = objTimeActivity.iID;
                    objTimeActDesc.sMonday = monday;
                    objTimeActDesc.sTuesday = tuesday;
                    objTimeActDesc.sWed = wed;
                    objTimeActDesc.sThu = thu;
                    objTimeActDesc.sFri = fri;
                    objTimeActDesc.sSat = sat;
                    objTimeActDesc.sSun = sun;
                    objTimeActDesc.sDescription = desc;
                    objTimeActDesc.bIsActive = true;
                    objTimeActDesc.Save();
                }
                DateTime mon;
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
                DayOfWeek today = DateTime.Now.DayOfWeek;
                DateTime sow = dtCurrent.AddDays(-(today - fdow)).Date;
                mon = sow.AddDays(1);
                string workHours = TimeCount(mon);
                var dot = workHours.Contains('.');
                long mm = 0;
                long hh = 0;
                if (dot)
                {
                    string[] str = workHours.Split('.');
                    string str1 = str[0];
                    string str2 = str[1];
                    hh = Convert.ToInt64(str1);
                   
                    if (str2.Length > 1)
                    {
                        mm = Convert.ToInt64(str2.Substring(0, 2));
                        if (mm > 60)
                        {
                            hh = hh + 1;
                            mm = mm - 60;
                        }
                    }
                    else
                    {
                        mm = Convert.ToInt64(str2);
                    }
                    workHours = hh + " : " + mm;
                }
                else
                {
                    workHours = workHours + " : 00";
                }

                return Json(workHours);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private SelectList getProjectList()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<ddlProjectTimeSheet> objProject = new List<ddlProjectTimeSheet>();
                List<cProjectAssigned> objProjectAssigned = cProjectAssigned.Find(" objEmpLogin = " + id);

                foreach (var item in objProjectAssigned)
                {
                    List<cProject> aobjProject = cProject.Find(" iID = " + item.objProject.iObjectID);
                    foreach (var itemProject in aobjProject)
                    {
                        objProject.Add(new ddlProjectTimeSheet { Value = itemProject.iID, Text = itemProject.sProjectName });
                    }

                }
                SelectList objProjectList = new SelectList(objProject, "Value", "Text");
                return objProjectList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getActivityList()
        {
            try
            {

                List<ddlActivity> objProject = new List<ddlActivity>();
                List<cActivity> objActivityName = cActivity.Find();

                foreach (var item in objActivityName)
                {
                    objProject.Add(new ddlActivity { Value = item.iID, Text = item.sName });
                }
                SelectList objAcitvityList = new SelectList(objProject, "Value", "Text");
                return objAcitvityList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult ReviewTimeSheet()
        {

            try
            {
                TimeSheet timeSheet = new TimeSheet();
                timeSheet.EmpList = getReviewEmpList();

                return View(timeSheet);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult ReviewTimeSheet(string empId)
        {

            try
            {
                List<TimeReview> lists = new List<TimeReview>();
                TimeSheet timeSheet = new TimeSheet();
                int id = Convert.ToInt32(HttpContext.User.Identity.Name);
                DateTime mon;
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
                DayOfWeek today = DateTime.Now.DayOfWeek;
                DateTime sow = DateTime.Now.AddDays(-(today - fdow)).Date;
                mon = sow;
                timeSheet.Mon = mon.ToString("dd/MM/yyyy");

                DateTime RStarts = new DateTime();
                DateTime EndDate = new DateTime();
                RStarts = mon.AddDays(1);
                EndDate = RStarts.AddDays(6);
                cEmpPersonalDetails empIdlogin = cEmpPersonalDetails.Get_ID(Convert.ToInt32(empId));
                int userId = empIdlogin.objEmpLogin.iObjectID;
                List<cEmpLogin> empLogin = cEmpLogin.Find(" objEmpLogin = " + id + " and iID = " + Convert.ToInt32(empId));


                if (empLogin.Count > 0)
                {
                    foreach (var item in empLogin)
                    {
                        List<cTimesheet> objTimes1 = cTimesheet.Find(" objEmpLogin = " + item.iID);
                        if (objTimes1.Count > 0)
                        {
                            foreach (var items in objTimes1)
                            {
                                TimeReview tm = new TimeReview();

                                List<cTimesheet> objTime = cTimesheet.Find(" iID = " + items.iID + " and dtStartDate = " + RStarts + " and dtEndDate = " + EndDate);

                                if (objTime.Count > 0)
                                {


                                    foreach (var itemTS in objTime)
                                    {
                                        List<cTimeSheetActivity> objActivity = cTimeSheetActivity.Find(" objTimesheet = " + itemTS.iID);

                                        if (objActivity.Count > 0)
                                        {
                                            foreach (var itemTSA in objActivity)
                                            {
                                                List<cTimeSheetActivityDes> objDes = cTimeSheetActivityDes.Find(" objTimeSheetActivity = " + itemTSA.iID);
                                                if (objDes.Count > 0)
                                                {
                                                    foreach (var itemDes in objDes)
                                                    {
                                                        string st = ReviewTimeCount(RStarts, Convert.ToInt32(empId));
                                                        tm.Total = st;
                                                        tm.date = RStarts.ToString("dd/MM/yyyy");
                                                        DateTime dts=RStarts;
                                                        tm.sunday = dts.AddDays(6).ToString("dd/MM/yyyy");
                                                        tm.status = "Save";
                                                    }
                                                }
                                            }

                                        }//TimeSheetLoop
                                    }
                                }//TimeSheet
                                lists.Add(tm);
                                RStarts = RStarts.AddDays(-7);
                                EndDate = EndDate.AddDays(-7);
                            }
                        }
                    }

                }

                return Json(lists);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private string ReviewTimeCount(DateTime monday, int id)
        {
            try
            {
                DateTime sunday = monday.AddDays(6);
                // DateTime stDate = DateTime.ParseExact(sunday.ToString(), "MM/dd/yyyy", null);
                DateTime stDate = monday;
                // DateTime stDate = DateTime.ParseExact(sunday.ToString(), "yyyy-MM-dd HH:mm:ss", null); 
                // DateTime enDate = DateTime.ParseExact(monday.ToString(), "MM/dd/yyyy", null);
                DateTime enDate = stDate.AddDays(6);
                double count = 0;
                int Minut = 0;
                List<cTimesheet> objTimeSheet = cTimesheet.Find(" objEmpLogin = " + id + " and dtStartDate = " + stDate + " and dtEndDate = " + enDate);
                if (objTimeSheet.Count > 0)
                {
                    List<cTimeSheetActivity> objTimeSheetActivity = cTimeSheetActivity.Find(" objTimesheet = " + objTimeSheet[0].iID);
                    if (objTimeSheetActivity.Count > 0)
                    {
                        foreach (var item in objTimeSheetActivity)
                        {
                            List<cTimeSheetActivityDes> objDesc = cTimeSheetActivityDes.Find(" objTimeSheetActivity = " + item.iID);
                            if (objDesc.Count > 0)
                            {
                                foreach (var items in objDesc)
                                {
                                    if (items.sMonday != "")
                                    {
                                        string strMon = items.sMonday;
                                        string[] monT = strMon.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sTuesday != "")
                                    {
                                        string strTue = items.sTuesday;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sWed != "")
                                    {
                                        string strTue = items.sWed;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sThu != "")
                                    {
                                        string strTue = items.sThu;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sFri != "")
                                    {
                                        string strTue = items.sFri;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sSat != "")
                                    {
                                        string strTue = items.sSat;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }
                                    if (items.sSun != "")
                                    {
                                        string strTue = items.sSun;
                                        string[] monT = strTue.Split(':');
                                        Minut = Minut + Convert.ToInt32(monT[0]) * 60 + Convert.ToInt32(monT[1]);
                                    }

                                }
                            }

                        }
                    }


                }
                count = Minut;
                double dt = count / 60;
                return Convert.ToString(Math.Round(dt));

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private SelectList getReviewEmpList()
        {
            try
            {


                int id = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<ddlEmp> objEmp = new List<ddlEmp>();

                List<cEmpLogin> aobjEmp = cEmpLogin.Find(" objEmpLogin = " + id);
                if (aobjEmp.Count > 0)
                {
                    foreach (var itemEmp in aobjEmp)
                    {
                        List<cEmpPersonalDetails> objPersonal = cEmpPersonalDetails.Find(" objEmpLogin = " + itemEmp.iID);

                        if (objPersonal.Count > 0)
                        {
                            foreach (var items in objPersonal)
                            {
                                objEmp.Add(new ddlEmp { Value = itemEmp.iID, Text = items.sFirstName });
                            }
                        }

                    }
                }

                SelectList objEmpList = new SelectList(objEmp, "Value", "Text");
                return objEmpList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}