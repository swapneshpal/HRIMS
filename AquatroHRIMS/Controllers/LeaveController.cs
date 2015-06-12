using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRIMS;
using AquatroHRIMS.ViewModel;
using AquatroHRIMS.Models;
using System.Data;
using AquatroHRIMS.ActionFilters;
using System.IO;
using System.Configuration;
using AquatroHRIMS.App_Code;

namespace AquatroHRIMS.Controllers
{
    [HRIMSActionFilter]
    [CustomException]
    public class LeaveController : Controller
    {
        //
        // GET: /Leave/
        string url = ConfigurationManager.AppSettings["URL"].ToString();// GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Leave/
        [HttpGet]
        public ActionResult CreateHolidayList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateHolidayList(HolidayList objholidaylist)
        {
            try
            {
                cHolidayList objholiday = cHolidayList.Create();
                objholiday.sOccassion = objholidaylist.Occassion;
                objholiday.dtOccDate = Convert.ToDateTime(objholidaylist.Date);
                objholiday.sDescription = objholidaylist.Description;
                objholiday.Save();
                Session["Result"] = "1";
                LeaveViewModel objleaveviewmodel = new LeaveViewModel();
                return RedirectToAction("HolidayList");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public ActionResult HolidayList()
        {
            try
            {
                List<cHolidayList> aobjHolidaylist = cHolidayList.Find();

                List<HolidayList> objHoliday = new List<HolidayList>();

                foreach (var item in aobjHolidaylist)
                {
                    objHoliday.Add(new HolidayList {id=item.iID, Occassion = item.sOccassion, Date = item.dtOccDate.ToString("dd/MM/yyyy"), Description = item.sDescription });
                }
                return View(objHoliday);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            try
            {
                LeaveViewModel objLeaveViewModel = new LeaveViewModel();
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                int ReportHeadID = cEmpLogin.Get_ID(LoginID).objEmpLogin.iObjectID;
                List<cEmpPersonalDetails> objPersona = cEmpPersonalDetails.Find(" objEmpLogin = " + ReportHeadID);

                DataTable dt = cEmpLeave.GetEmpDetail(LoginID);
                if (dt.Rows.Count > 0)
                {
                    objLeaveViewModel.EmployeeCode = dt.Rows[0]["EmployeeCode"].ToString();
                    objLeaveViewModel.EmployeeDepartment = dt.Rows[0]["DepartmentType"].ToString();
                    objLeaveViewModel.EmployeeDesignation = dt.Rows[0]["Designation"].ToString();
                    objLeaveViewModel.EmployeeName = dt.Rows[0]["Firstname"].ToString();
                    if (objPersona.Count > 0)
                    {
                        objLeaveViewModel.EmployeeReportingHead = objPersona[0].sFirstName;
                    }
                    else
                    {
                        objLeaveViewModel.EmployeeReportingHead = "";
                    }

                }
                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(LoginID);
                objLeaveViewModel.LeaveTypeList = getLeaveTypeList();

                return View(objLeaveViewModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public JsonResult CreateRequest(LeaveViewModel objViewModel)
        {
            try
            {
                cEmpLeave objempleave = cEmpLeave.Create();
                string LeavetypeID = objViewModel.SelectedLeaveType[0].ToString();
                objempleave.objLeaveType.iObjectID = Convert.ToInt32(LeavetypeID);
                objempleave.dtFromDate = objViewModel.objEmployeeLeave.FromDate;
                objempleave.dtToDate = objViewModel.objEmployeeLeave.ToDate;
                objempleave.sReason = objViewModel.objEmployeeLeave.Reason;
                objempleave.sLeaveStatus = "2";
                objempleave.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                objempleave.iReportingHead = cEmpLogin.Get_ID(Convert.ToInt32(HttpContext.User.Identity.Name)).objEmpLogin.iObjectID;
                objempleave.bIsActive = true;
                objempleave.Save();

                int IreportHead = cEmpLogin.Get_ID(Convert.ToInt32(HttpContext.User.Identity.Name)).objEmpLogin.iObjectID;
                string reporterEmail = cEmpLogin.Get_ID(IreportHead).sEmailID;

                List<cEmpPersonalDetails> aobOer = cEmpPersonalDetails.Find(" objEmpLogin = " + Convert.ToInt32(HttpContext.User.Identity.Name));
                string ReprtingFrom = aobOer[0].sFirstName + " " + aobOer[0].sLastName;
                List<cEmpPersonalDetails> aobre = cEmpPersonalDetails.Find(" objEmpLogin = " + objempleave.iReportingHead);
                string ReprtingHead= aobre[0].sFirstName + " " + aobre[0].sLastName;
                MailLeaveRequest(ReprtingHead, ReprtingFrom, reporterEmail);
                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public ActionResult MyLeave()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cEmpLeave.GetMyLeave(LoginID);
                List<PendingLeave> objLeaveMo = new List<PendingLeave>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLeaveMo.Add(new PendingLeave { Days = dt.Rows[i]["Days"].ToString(), EmpName = dt.Rows[i]["Firstname"].ToString(), LeaveType = dt.Rows[i]["LeaveType"].ToString(), LeaveStatus = dt.Rows[i]["Status"].ToString() });
                }
                return View(objLeaveMo);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult LeaveBalance()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cEmpLeave.GetPendinLeave(-2);
                List<PendingLeave> objLeaveMo = new List<PendingLeave>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLeaveMo.Add(new PendingLeave { Days = dt.Rows[i]["Days"].ToString(), EmpName = dt.Rows[i]["Firstname"].ToString(), LeaveType = dt.Rows[i]["LeaveType"].ToString(), LeaveStatus = dt.Rows[i]["Status"].ToString() });
                }
                return View(objLeaveMo);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private SelectList getLeaveTypeList()
        {
            try
            {
                List<ddlLeaveType> objLeaveList = new List<ddlLeaveType>();

                List<cLeaveType> objLeaveType = cLeaveType.Find();

                foreach (var itememp in objLeaveType)
                {
                    objLeaveList.Add(new ddlLeaveType { Value = itememp.iID, Text = itememp.sName });
                }
                SelectList objLeaveTypeList = new SelectList(objLeaveList, "Value", "Text");
                return objLeaveTypeList;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult PendingLeaves()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cEmpLeave.GetPendinLeave(LoginID);
                List<PendingLeave> objLeaveMo = new List<PendingLeave>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objLeaveMo.Add(new PendingLeave { LeaveID = dt.Rows[i]["LeaveID"].ToString(), Days = dt.Rows[i]["Days"].ToString(), EmpName = dt.Rows[i]["Firstname"].ToString(), LeaveType = dt.Rows[i]["LeaveType"].ToString(), LeaveStatus = dt.Rows[i]["Status"].ToString() });
                }
                return View(objLeaveMo);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult ViewRequest(int ID)
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                PendingLeave objpend = new PendingLeave();
                cEmpLeave objLeave = cEmpLeave.Get_ID(ID);
                if (objLeave == null)
                {
                    return RedirectToAction("Signout", "Login");
                }
                else
                {
                    if (objLeave.iReportingHead != loginID)
                    {
                        return RedirectToAction("Signout", "Login");
                    }

                }
                objpend.LeaveReason = objLeave.sReason;
                objpend.LeaveID = ID.ToString();
                return View(objpend);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult ApproveLeave(PendingLeave pd)
        {
            try
            {
                cEmpLeave objLeave = cEmpLeave.Get_ID(Convert.ToInt32(pd.LeaveID));
                objLeave.sLeaveStatus = "4";
                objLeave.Save();
                return RedirectToAction("PendingLeaves");
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public JsonResult Delete(string ID)
        {
            try
            {
                int id = Convert.ToInt32(ID);
                cHolidayList.Delete(id);

                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //Send Mail To Reporting head:- 
        public void MailLeaveRequest(string ReprtingHead, string ReprtingFrom, string Email)
        {
            try
            {
                string link = url + "/Login";
                string Links = "<b><a target='_blank' href=" + link + ">Click here to Login.</a></b>";
                string strBody = "";
                StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/LeaveRequest.html"));
                strBody = sr.ReadToEnd();
                sr.Close();

                strBody = strBody.Replace("#Link#", link);
                strBody = strBody.Replace("#EmployeeName#", ReprtingFrom);
                strBody = strBody.Replace("#ReprterHead#", ReprtingHead);
                string str = Mail.SendEmail(Email, "Employee Leave Request", strBody, true);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}