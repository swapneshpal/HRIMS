using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.Models;
using HRIMS;
using AquatroHRIMS.ViewModel;
using System.Data;
using AquatroHRIMS.ActionFilters;
using System.IO;
using AquatroHRIMS.App_Code;
using System.Configuration;

namespace AquatroHRIMS.Controllers
{
    [HRIMSActionFilter]
    [CustomException]
    public class EmployeeController : Controller
    {
        //
        string url = ConfigurationManager.AppSettings["URL"].ToString();// GET: /Employee/


        [HttpGet]
        public ActionResult Add(string ID)
        {
            try
            {
                EmployeeViewModel objEmpViewMod = new EmployeeViewModel();
                if (ID != null)
                {
                    int val = Convert.ToInt32(ID);
                    cEmpLogin objEmpLogin = cEmpLogin.Get_ID(val);
                    List<cEmpPersonalDetails> aobPerso = cEmpPersonalDetails.Find(" objEmpLogin = " + objEmpLogin.iID);
                    List<cEmployee> aobEmp = cEmployee.Find(" objEmpLogin = " + objEmpLogin.iID);
                    cEmpDesigDepartmentType objempdes = cEmpDesigDepartmentType.Get_ID(aobEmp[0].objEmpDesigDepartmentType.iObjectID);
                    objEmpViewMod.EmployeeEmailIdUpdate = objEmpLogin.sEmailID;
                    objEmpViewMod.PersonalEmailUpdate = aobPerso[0].sPersoanlEmailID;
                    objEmpViewMod.hdnEmployeeID = objEmpLogin.iID.ToString();
                    ViewBag.FirstName = aobPerso[0].sFirstName;
                    ViewBag.LastName = aobPerso[0].sLastName;
                    ViewBag.MiddleName = aobPerso[0].sMiddleName;
                    ViewBag.DOJ = aobEmp[0].dtDOJ.ToString("dd/MM/yyyy");
                    ViewBag.TitleName = aobEmp[0].objTitle.iObjectID;
                    ViewBag.iLocation = aobEmp[0].iJobLocation;
                    ViewBag.iReportingHead = objEmpLogin.objEmpLogin.iObjectID;
                    ViewBag.RollAccessID = objEmpLogin.objRoleAccess.iObjectID;
                    ViewBag.DepID = cDepartmentType.Get_ID(objempdes.objDepartmentType.iObjectID).objDepartment.iObjectID;
                    ViewBag.DepTypeID = objempdes.objDepartmentType.iObjectID;
                    ViewBag.DesgID = objempdes.objDesignation.iObjectID;

                    objEmpViewMod.DesignationList = getDesignationList();
                    objEmpViewMod.ReportList = getReportHeadList();
                    objEmpViewMod.TitleList = getTitleList();
                    objEmpViewMod.LocationList = getLocation();
                    objEmpViewMod.DepartmentList = getDepartmentList();
                    objEmpViewMod.RollAccessList = getRollAccessList();
                    objEmpViewMod.DepartTypeList = getDepartmentType();
                    objEmpViewMod.EmpList = GetEmployeeList();
                    ViewBag.Update = objEmpViewMod;

                    return View(objEmpViewMod);
                }
                else
                {
                    objEmpViewMod.DesignationList = getDesignationList();
                    objEmpViewMod.ReportList = getReportHeadList();
                    objEmpViewMod.TitleList = getTitleList();
                    objEmpViewMod.LocationList = getLocation();
                    objEmpViewMod.DepartmentList = getDepartmentList();
                    objEmpViewMod.RollAccessList = getRollAccessList();
                    objEmpViewMod.DepartTypeList = getDepartmentType();
                    objEmpViewMod.EmpList = GetEmployeeList();
                    return View(objEmpViewMod);

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public JsonResult GetData(string DepId)
        {
            try
            {
                JsonResult result = new JsonResult();
                int val = Convert.ToInt32(DepId);
                List<ddlDepartmentType> objDepartTypeLst = new List<ddlDepartmentType>();
                List<cDepartmentType> objDepartType = cDepartmentType.Find(" objDepartment = " + val);
                foreach (var item in objDepartType)
                {
                    objDepartTypeLst.Add(new ddlDepartmentType { Value = item.iID, Text = item.sName });
                }
                SelectList objddpt = new SelectList(objDepartTypeLst, "Value", "Text");
                result.Data = objddpt;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
                //return Json(objEmpViewMod);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private SelectList getDesignationList()
        {
            try
            {

                List<ddlDesignation> objDesign = new List<ddlDesignation>();
                List<cDesignation> objDesignation = cDesignation.Find();

                foreach (var itemDesgn in objDesignation)
                {
                    objDesign.Add(new ddlDesignation { Value = itemDesgn.iID, Text = itemDesgn.sName });
                }
                SelectList objDesigList = new SelectList(objDesign, "Value", "Text");
                return objDesigList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getReportHeadList()
        {
            try
            {
                List<ddlReportHead> objReportHeadList = new List<ddlReportHead>();

                List<cEmpLogin> aobEmpLogin = cEmpLogin.Find(" objRoleAccess = 1 or objRoleAccess = 2 or objRoleAccess = 5 ");

                for(int i=0;i<aobEmpLogin.Count;i++)
                {
                    List<cEmpPersonalDetails> objReportHead = cEmpPersonalDetails.Find("objEmpLogin = " + aobEmpLogin[i].iID);

                    foreach (var item in objReportHead)
                    {
                        objReportHeadList.Add(new ddlReportHead { Value = item.objEmpLogin.iObjectID, Text = item.sFirstName });
                    }
                }

                SelectList objReportList = new SelectList(objReportHeadList, "Value", "Text");
                return objReportList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getDepartmentList()
        {
            try
            {
                List<ddlDepartment> objDepartmentList = new List<ddlDepartment>();
                List<cDepartment> objDepartment = cDepartment.Find();

                foreach (var item in objDepartment)
                {
                    objDepartmentList.Add(new ddlDepartment { Value = item.iID, Text = item.sName });
                }
                SelectList objDepartmenttList = new SelectList(objDepartmentList, "Value", "Text");
                return objDepartmenttList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getRollAccessList()
        {
            try
            {
                List<ddlRollAccess> objRollAccessList = new List<ddlRollAccess>();
                List<cRoleAccess> objRollAccess = cRoleAccess.Find();

                foreach (var item in objRollAccess)
                {
                    objRollAccessList.Add(new ddlRollAccess { Value = item.iID, Text = item.sName });
                }
                SelectList objRollList = new SelectList(objRollAccessList, "Value", "Text");
                return objRollList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private SelectList getTitleList()
        {
            try
            {
                List<ddlEmpTitle> objTitleList = new List<ddlEmpTitle>();
                List<cTitle> objTitle = cTitle.Find();

                foreach (var item in objTitle)
                {
                    objTitleList.Add(new ddlEmpTitle { Value = item.iID, Text = item.sName });
                }
                SelectList objTitles = new SelectList(objTitleList, "Value", "Text");
                return objTitles;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private SelectList getLocation()
        {
            try
            {
                List<ddlLocation> objCountryList = new List<ddlLocation>();
                List<cCountry> objCountry = cCountry.Find();

                foreach (var item in objCountry)
                {
                    objCountryList.Add(new ddlLocation { Value = item.iID, Text = item.sName });
                }
                SelectList objCountries = new SelectList(objCountryList, "Value", "Text");
                return objCountries;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private SelectList getDepartmentType()
        {
            try
            {
                List<ddlDepartmentType> objDepartTypeLst = new List<ddlDepartmentType>();
                List<cDepartmentType> objDepartType = cDepartmentType.Find();

                foreach (var item in objDepartType)
                {
                    objDepartTypeLst.Add(new ddlDepartmentType { Value = item.iID, Text = item.sName });
                }
                SelectList objddpt = new SelectList(objDepartTypeLst, "Value", "Text");
                return objddpt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public JsonResult Add(EmployeeViewModel emp, string hdnEmployeeID)
        {
            try
            {
                EmployeeViewModel objEmpViewMod = new EmployeeViewModel();
                if (emp.SelectedRollAccess[0] == "" || emp.SelectedReportHead[0] == "" || emp.SelectedLocation[0] == "" || emp.SelectedDepartmentType[0] == "" || emp.SelectedDesignation[0] == "" || emp.SelectedRollAccess[0] == "" || emp.SelectedReportHead[0] == "")
                {
                    return Json("1");//Some Dropdown are not selected
                }
                else
                {

                    //if (ModelState.IsValid)
                    //{
                    if (hdnEmployeeID != null && hdnEmployeeID != "")
                    {
                        int ID = Convert.ToInt32(hdnEmployeeID);
                        cEmpLogin objEmpLogin = cEmpLogin.Get_ID(ID);
                        objEmpLogin.sEmailID = emp.EmployeeEmailIdUpdate;
                        objEmpLogin.objRoleAccess.iObjectID = Convert.ToInt32(emp.SelectedRollAccess[0]);
                        objEmpLogin.objEmpLogin.iObjectID = Convert.ToInt32(emp.SelectedReportHead[0]);
                        objEmpLogin.sFirstTime = "1";
                        objEmpLogin.bIsActive = true;
                        objEmpLogin.sPassword = emp.Password;
                        objEmpLogin.Save();

                        List<cEmpPersonalDetails> aobjEmployeePersonalDetails = cEmpPersonalDetails.Find(" objEmpLogin = " + objEmpLogin.iID);
                        aobjEmployeePersonalDetails[0].sFirstName = emp.EmpPersonal.FirstName;
                        if (emp.EmpPersonal.MiddleName == null)
                        {
                            aobjEmployeePersonalDetails[0].sMiddleName = "";
                        }
                        else
                        {
                            aobjEmployeePersonalDetails[0].sMiddleName = aobjEmployeePersonalDetails[0].sMiddleName;
                        }
                        aobjEmployeePersonalDetails[0].sMiddleName = emp.EmpPersonal.MiddleName;

                        aobjEmployeePersonalDetails[0].sLastName = emp.EmpPersonal.LastName;
                        aobjEmployeePersonalDetails[0].sPersoanlEmailID = emp.PersonalEmailUpdate;
                        aobjEmployeePersonalDetails[0].objEmpLogin.iObjectID = objEmpLogin.iID;
                        aobjEmployeePersonalDetails[0].Save();


                        List<cEmployee> aobjEmp = cEmployee.Find(" objEmpLogin = " + objEmpLogin.iID);
                        aobjEmp[0].objEmpLogin.iObjectID = objEmpLogin.iID;
                        aobjEmp[0].sEmployeeID = aobjEmp[0].sEmployeeID;
                        aobjEmp[0].objEmpLogin.iObjectID = Convert.ToInt32(objEmpLogin.iID);
                        aobjEmp[0].objTitle.iObjectID = Convert.ToInt32(emp.SelectedTitle[0]);
                        aobjEmp[0].iJobLocation = Convert.ToInt32(emp.SelectedLocation[0]);
                        aobjEmp[0].dtDOJ = Convert.ToDateTime(emp.Employee.DOJ);
                        aobjEmp[0].Save();
                        List<cEmpDesigDepartmentType> aobjDesigDepart = cEmpDesigDepartmentType.Find(" iID = " + aobjEmp[0].objEmpDesigDepartmentType.iObjectID);
                        aobjDesigDepart[0].objDepartmentType.iObjectID = Convert.ToInt32(emp.SelectedDepartmentType[0]);
                        aobjDesigDepart[0].objDesignation.iObjectID = Convert.ToInt32(emp.SelectedDesignation[0]);
                        aobjDesigDepart[0].Save();
                        MailCreateEmployee(objEmpLogin.sEmailID, objEmpLogin.sPassword, "Update");
                        return Json("2");//Update
                    }
                    else
                    {
                        cEmpLogin objEmpLogin = cEmpLogin.Create();
                        objEmpLogin.sEmailID = emp.EmployeeEmailId;
                        objEmpLogin.objRoleAccess.iObjectID = Convert.ToInt32(emp.SelectedRollAccess[0]);
                        objEmpLogin.objEmpLogin.iObjectID = Convert.ToInt32(emp.SelectedReportHead[0]);
                        objEmpLogin.sFirstTime = "1";
                        objEmpLogin.bIsActive = true;
                        objEmpLogin.sPassword = emp.Password;
                        objEmpLogin.Save();

                        cEmpPersonalDetails objEmployeePersonalDetails = cEmpPersonalDetails.Create();
                        objEmployeePersonalDetails.sFirstName = emp.EmpPersonal.FirstName;
                        if (emp.EmpPersonal.MiddleName == null)
                        {
                            objEmployeePersonalDetails.sMiddleName = "";
                        }
                        else
                        {
                            objEmployeePersonalDetails.sMiddleName = emp.EmpPersonal.MiddleName;
                        }

                        objEmployeePersonalDetails.sLastName = emp.EmpPersonal.LastName;
                        objEmployeePersonalDetails.sPersoanlEmailID = emp.PersonalEmail;
                        objEmployeePersonalDetails.objEmpLogin.iObjectID = objEmpLogin.iID;

                        objEmployeePersonalDetails.Save();
                        cEmpDesigDepartmentType objDesigDepart = cEmpDesigDepartmentType.Create();
                        objDesigDepart.objDepartmentType.iObjectID = Convert.ToInt32(emp.SelectedDepartmentType[0]);
                        objDesigDepart.objDesignation.iObjectID = Convert.ToInt32(emp.SelectedDesignation[0]);
                        objDesigDepart.Save();

                        cEmployee objEmp = cEmployee.Create();
                        objEmp.objEmpLogin.iObjectID = objEmpLogin.iID;
                        objEmp.objEmpDesigDepartmentType.iObjectID = objDesigDepart.iID;
                        objEmp.objEmpLogin.iObjectID = Convert.ToInt32(objEmpLogin.iID);
                        objEmp.iJobLocation = Convert.ToInt32(emp.SelectedLocation[0]);
                        objEmp.objTitle.iObjectID = Convert.ToInt32(emp.SelectedTitle[0]);
                        objEmp.dtDOJ = Convert.ToDateTime(emp.Employee.DOJ);
                        objEmp.Save();
                        MailCreateEmployee(objEmpLogin.sEmailID, objEmpLogin.sPassword, "Create");
                        return Json("3");//Create

                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        public List<EmployeeList> GetEmployeeList()
        {
            try
            {
                DataTable dt = cEmployee.GetEmpList();
                List<EmployeeList> objEmpList = new List<EmployeeList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objEmpList.Add(new EmployeeList { EmpID = dt.Rows[i]["EmpID"].ToString(), EmployeeCode = dt.Rows[i]["EmployeeCode"].ToString(), EmployeeName = dt.Rows[i]["Firstname"].ToString(), Designation = dt.Rows[i]["Designation"].ToString(), DepartmentType = dt.Rows[i]["DepartmentType"].ToString(), WorkEmail = dt.Rows[i]["WorkEmail"].ToString(), DOJ = Convert.ToDateTime(dt.Rows[i]["DOJ"]).ToString("dd/MM/yyyy") });
                }
                return objEmpList;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //Profile Email & Mobile Check:-

        public ActionResult EmployeeList()
        {

            try
            {
                DataTable dt = cEmployee.GetEmpList();
                List<EmployeeList> objEmpList = new List<EmployeeList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objEmpList.Add(new EmployeeList { EmpID = dt.Rows[i]["EmpID"].ToString(), EmployeeCode = dt.Rows[i]["EmployeeCode"].ToString(), EmployeeName = dt.Rows[i]["Firstname"].ToString(), Designation = dt.Rows[i]["Designation"].ToString(), DepartmentType = dt.Rows[i]["DepartmentType"].ToString(), WorkEmail = dt.Rows[i]["WorkEmail"].ToString(), DOJ = Convert.ToDateTime(dt.Rows[i]["DOJ"]).ToString("dd/MM/yyyy") });
                }
                return View(objEmpList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public JsonResult CheckWorkEmail(string EmployeeEmailId)
        {

            try
            {
                List<cEmpLogin> objLogin = cEmpLogin.Find(" sEmailID = " + EmployeeEmailId.ToString().Trim());
                if (objLogin.Count > 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }


        }
        [HttpGet]
        public JsonResult CheckPersonalEmail(string PersonalEmail)
        {

            try
            {
                List<cEmpPersonalDetails> objUserprofile = cEmpPersonalDetails.Find(" sPersoanlEmailID = " + PersonalEmail);
                if (objUserprofile.Count > 0)
                {

                    return Json(false, JsonRequestBehavior.AllowGet);


                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        //public JsonResult Edit(string ID)
        //{
        //    try
        //    {
        //        JsonResult result = new JsonResult();

        //        result.Data = objEmpViewMod;
        //        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        public JsonResult Delete(string ID)
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                int id = Convert.ToInt32(ID);
                if (loginID == id)
                {
                    return Json("2");
                }

                //Profile Detail Delete

                List<cEmpPersonalDetails> objPersonal = cEmpPersonalDetails.Find(" objEmpLogin = " + id);
                if (objPersonal.Count > 0)
                {
                    foreach (var emp in objPersonal)
                    {
                        cEmpPersonalDetails.Delete(emp.iID);
                    }
                 }
                List<cEmpProfessionalDetail> objProf = cEmpProfessionalDetail.Find(" objEmpLogin = " + id);
                if (objProf.Count > 0)
                {
                    foreach (var emp in objProf)
                    {
                        cEmpProfessionalDetail.Delete(emp.iID);
                    }
                }

                List<cEmpEducationDetail> objEdu= cEmpEducationDetail.Find(" objEmpLogin = " + id);
                if (objEdu.Count > 0)
                {
                    foreach (var emp in objEdu)
                    {
                        cEmpProfessionalDetail.Delete(emp.iID);
                    }
                }
                List<cEmpEmergencyContact> objEmrCont = cEmpEmergencyContact.Find(" objEmpLogin = " + id);
                if (objEmrCont.Count > 0)
                {
                    foreach (var emp in objEmrCont)
                    {
                        cEmpProfessionalDetail.Delete(emp.iID);
                    }
                }
                List<cEmployee> aobEmp = cEmployee.Find(" objEmpLogin = " + id);
                if (aobEmp.Count > 0)
                {
                    foreach (var itemavEmp in aobEmp)
                    {
                        cEmpDesigDepartmentType.Delete(itemavEmp.objEmpDesigDepartmentType.iObjectID);
                        cEmployee.Delete(itemavEmp.iID);
                    }
                }
                //Leave  Delete Part:-
                List<cEmpGoal> aobEmpGoal = cEmpGoal.Find(" objEmpLogin = " + id);
                if (aobEmpGoal.Count > 0)
                {
                    foreach (var itemavGoal in aobEmpGoal)
                    {
                        cEmpGoal.Delete(itemavGoal.iID);
                    }
                }
                //Leave  Delete Part:-
                List<cEmpAvailedLeave> aobAvailLeave = cEmpAvailedLeave.Find(" objEmpLogin = " + id);
                if (aobAvailLeave.Count > 0)
                {
                    foreach (var itemavLeave in aobAvailLeave)
                    {
                        cEmpAvailedLeave.Delete(itemavLeave.iID);
                    }
                }

                //Project Assgned Delete Part:-
                List<cProjectAssigned> objProjectAssigned = cProjectAssigned.Find(" objEmpLogin = " + id);
                if (objProjectAssigned.Count > 0)
                {
                    foreach (var itemProAss in objProjectAssigned)
                    {
                        cProjectAssigned.Delete(itemProAss.iID);
                    }
                }
                //Permission  Delete Part:-

                List<cPermissionEmpLogin> objPermissLog = cPermissionEmpLogin.Find(" objEmpLogin = " + id);
                if (objPermissLog.Count > 0)
                {
                    foreach (var itemProAss in objPermissLog)
                    {
                        cPermissionEmpLogin.Delete(itemProAss.iID);
                    }
                }

                //Time Sheet Delete Part:-
                List<cTimesheet> aobjTimeSheet = cTimesheet.Find(" objEmpLogin = " + id);
                if (aobjTimeSheet.Count > 0)
                {

                    List<cTimeSheetActivity> aobjTimeActivity = cTimeSheetActivity.Find(" objTimesheet = " + aobjTimeSheet[0].iID);
                    if (aobjTimeActivity.Count>0)
                    {
                        foreach (var itemAct in aobjTimeActivity)
                        {
                            List<cTimeSheetActivityDes> aobjTimeAcDes = cTimeSheetActivityDes.Find(" objTimeSheetActivity = " + itemAct.iID);
                            if (aobjTimeAcDes.Count > 0)
                            {
                                foreach (var itemDes in aobjTimeAcDes)
                                {
                                    cTimeSheetActivityDes.Delete(itemDes.iID);
                                }
                            }
                            cTimeSheetActivity.Delete(itemAct.iID);
                            
                        }

                    
                    }
                    cTimesheet.Delete(aobjTimeSheet[0].iID);

                }

                //Update reporting Head:-
                List<cEmpLogin> aobEmpLog = cEmpLogin.Find(" objEmpLogin = " + id);
                if (aobEmpLog.Count > 0)
                {
                    foreach (var itemEmpLog in aobEmpLog)
                    {
                        itemEmpLog.objEmpLogin.iObjectID = 1;
                        itemEmpLog.Save();
                    }
                }
                List<cEmpLeave> aobEmpReport = cEmpLeave.Find(" iReportingHead = " + id);
                if (aobEmpReport.Count > 0)
                {
                    foreach (var itemEmpRe in aobEmpReport)
                    {
                        itemEmpRe.iReportingHead = 1;
                        itemEmpRe.Save();
                    }
                }
                cEmpLogin.Delete(id);

                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult CheckWorkEmailUpdate(string EmployeeEmailIdUpdate, string hdnEmployeeID)
        {
            try
            {
                int ID = Convert.ToInt32(hdnEmployeeID);
                List<cEmpLogin> objLogin = cEmpLogin.Find(" sEmailID = " + EmployeeEmailIdUpdate.ToString().Trim());
                if (objLogin.Count > 0 && objLogin[0].iID != ID)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult CheckPersoanlEmailUpdate(string PersonalEmailUpdate, string hdnEmployeeID)
        {
            try
            {
                int ID = Convert.ToInt32(hdnEmployeeID);
                List<cEmpPersonalDetails> objUserprofile = cEmpPersonalDetails.Find(" sPersoanlEmailID = " + PersonalEmailUpdate);
                if (objUserprofile.Count > 0 && objUserprofile[0].objEmpLogin.iObjectID != ID)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);




            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void MailCreateEmployee(string Email, string Password, string Action)
        {
            try
            {
                if (Action == "Create")
                {

                    string link = url + "/Login";
                    string Links = "<b><a target='_blank' href=" + link + ">Click here to Login.</a></b>";
                    string strBody = "";
                    StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/CreateAccount.html"));
                    strBody = sr.ReadToEnd();
                    sr.Close();
                    
                    strBody = strBody.Replace("#Link#", Links);
                    strBody = strBody.Replace("#Employee#", Email);
                    strBody = strBody.Replace("#UserName#", Email);
                    strBody = strBody.Replace("#password#", Password);
                    string str = Mail.SendEmail(Email, "Account Details", strBody, true);

                }
                else
                {

                    string link = url + "/Login";
                    string Links = "<b><a target='_blank' href=" + link + ">Click here to Login.</a></b>";
                    string strBody = "";
                    StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/CreateAccount.html"));
                    strBody = sr.ReadToEnd();
                    sr.Close();
                    strBody = strBody.Replace("#Link#", Links);
                    strBody = strBody.Replace("#Employee#", Email);
                    strBody = strBody.Replace("#UserName#", Email);
                    strBody = strBody.Replace("#password#", Password);
                    string str = Mail.SendEmail(Email, "Account Update ", strBody, true);
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }


        }

    }
}