using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.Models;
using HRIMS;
using AquatroHRIMS.ViewModel;
using System.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using AquatroHRIMS.ActionFilters;


namespace AquatroHRIMS.Controllers
{
    //[HRIMSActionFilter]
    [CustomException]
    public class QuadrantMeasuresController : Controller
    {
        QuadrantMeasuresViewModel objQuadrantMeasure = new QuadrantMeasuresViewModel();
        private SelectList getDepartmentTypeID()
        {
            try
            {
                List<ddlDepartmentTypeList> objDeptTypeList = new List<ddlDepartmentTypeList>();
                List<cDepartmentType> objDeptType = cDepartmentType.Find();
                objDeptTypeList.Add(new ddlDepartmentTypeList { Value = 0, Text = "--select--" });
                foreach (var item in objDeptType)
                {
                    objDeptTypeList.Add(new ddlDepartmentTypeList { Value = item.iID, Text = item.sName });
                }

                SelectList objDeptTypeListData = new SelectList(objDeptTypeList, "Value", "Text");
                return objDeptTypeListData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private SelectList getGoalTitleList()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<ddlGoalTitleList> objGoalTitleList = new List<ddlGoalTitleList>();
                List<cGoals> objGoalTitle = cGoals.Find(" objEmpLogin = " + LoginID);

                foreach (var item in objGoalTitle)
                {
                    objGoalTitleList.Add(new ddlGoalTitleList { Value = item.iID, Text = item.sName });
                }

                SelectList objGoalList = new SelectList(objGoalTitleList, "Value", "Text");
                return objGoalList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private SelectList getStatusList()
        {
            try
            {
                List<ddlStatusList> objStatusList = new List<ddlStatusList>();
                List<cStatus> objStatus = cStatus.Find();

                foreach (var item in objStatus)
                {
                    objStatusList.Add(new ddlStatusList { Value = item.iID, Text = item.sName });
                }

                SelectList objStatusList1 = new SelectList(objStatusList, "Value", "Text");
                return objStatusList1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private SelectList getEmployeeList()
        {
            try
            {
                List<ddlEmployeeList1> objEmpList = new List<ddlEmployeeList1>();
                List<cEmpPersonalDetails> objEmp = cEmpPersonalDetails.Find();

                foreach (var item in objEmp)
                {
                    objEmpList.Add(new ddlEmployeeList1 { Value = item.iID, Text = item.sFirstName });
                }

                SelectList objEmployeeList = new SelectList(objEmpList, "Value", "Text");
                return objEmployeeList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private SelectList getRatingList()
        {
            try
            {
                List<ddlRatingsList> objReviewRatingList = new List<ddlRatingsList>();
                List<cRatings> objReviewRating = cRatings.Find();

                foreach (var item in objReviewRating)
                {
                    objReviewRatingList.Add(new ddlRatingsList { Value = item.iID, Text = item.sRatingName });
                }

                SelectList objRatingList = new SelectList(objReviewRatingList, "Value", "Text");
                return objRatingList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult setQuadrantMeasures()
        {
            try
            {
                objQuadrantMeasure.DepartmentTypeModel = getDepartmentTypeID();
                objQuadrantMeasure.GoalTileModel = getGoalTitleList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult setQuadrantMeasures(QuadrantMeasuresViewModel quadViewModel)
        {
            QuadrantMeasuresViewModel objMeasures = new QuadrantMeasuresViewModel();
            try
            {
                if (quadViewModel.SelectedDeptList.Count() > 0)
                {
                    foreach (var item in quadViewModel.SelectedDeptList)
                    {
                        cQuadrantMeasure objQuadrantMeasure = cQuadrantMeasure.Create();
                        objQuadrantMeasure.objDepartmentType.iObjectID = Convert.ToInt32(item.ToString());
                        objQuadrantMeasure.objGoals.iObjectID = Convert.ToInt32(quadViewModel.objEmpQudrants.objGoals);
                        objQuadrantMeasure.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                        objQuadrantMeasure.sMeasures = quadViewModel.objEmpQudrants.sMeasures;
                        objQuadrantMeasure.bIsActive = true;
                        objQuadrantMeasure.Save();
                        TempData["Result"] = 1;
                    }
                }

                objMeasures.DepartmentTypeModel = getDepartmentTypeID();
                objMeasures.GoalTileModel = getGoalTitleList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objMeasures);
        }
        public ActionResult ReviewQuadrants(string Val)
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                int userType = 0;
                if (Val != null)
                {
                    userType = Convert.ToInt32(Val);
                }
                DataTable dt = cQuadrantMeasure.getEmpQuadratantList(loginID, userType);
                List<EmpList> objEmpList = new List<EmpList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objEmpList.Add(new EmpList { EmployeeName = dt.Rows[i]["EmployeeName"].ToString(), Department = dt.Rows[i]["Department"].ToString(), Designation = dt.Rows[i]["Designation"].ToString(), empID = dt.Rows[i]["empId"].ToString() });
                }
                objQuadrantMeasure.empList = objEmpList;
                if (Val != null)
                {
                    JsonResult result = new JsonResult();
                    result.Data = objEmpList;
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
                return View(objQuadrantMeasure);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ReviewQuadrantDetails(int ID)
        {
            int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
            try
            {
                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(ID);
                int intReportingHead = objEmpLogin.objEmpLogin.iObjectID;

                cEmpLogin objemp = cEmpLogin.Get_ID(LoginID);
                string RoleAccessName = cRoleAccess.Get_ID(objemp.objRoleAccess.iObjectID).sName.ToString();

                if (RoleAccessName != "Admin")
                {
                    if (intReportingHead != LoginID)
                    {
                        return RedirectToAction("SignOut", "Login");
                    }
                }

                if (RoleAccessName == "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.ReportingHeadFlag = true;
                    objQuadrantMeasure.AdminFlag = true;
                }
                else if (RoleAccessName == "Admin" && intReportingHead != LoginID)
                {

                    objQuadrantMeasure.AdminFlag = true;
                    objQuadrantMeasure.ReportingHeadFlag = false;
                }
                else if (RoleAccessName != "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.AdminFlag = false;
                    objQuadrantMeasure.ReportingHeadFlag = true;
                }




                objQuadrantMeasure.EmployeeModel = getEmployeeList();
                objQuadrantMeasure.StatusModel = getStatusList();
                objQuadrantMeasure.hdnEmployeeID = ID;
                //// string abc = getQuadrantName();

                DataTable dt = cQuadrantMeasure.reviewEmpQuadratants(ID);
                List<lstEmpQuadrantList> list = new List<lstEmpQuadrantList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new lstEmpQuadrantList { GoalName = dt.Rows[i]["GoalName"].ToString(), Measures = dt.Rows[i]["Measures"].ToString(), Count = i + 1, GoalId = Convert.ToInt32(dt.Rows[i]["GoalId"].ToString()), StatusName = dt.Rows[i]["StatusName"].ToString(), empcomment = dt.Rows[i]["empcomment"].ToString(), EmpGoalID = Convert.ToInt32(dt.Rows[i]["EmpGoalID"].ToString()), ManagerComment = dt.Rows[i]["ManagerComment"].ToString(), ManagerFlag = Convert.ToBoolean(dt.Rows[i]["ManagerFlag"].ToString()) });
                }
                objQuadrantMeasure.QuadList = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult ReviewQuadrantDetails(QuadrantMeasuresViewModel objQuadViewModel)
        {
            int empID = objQuadViewModel.EmpUniqueGoalID[0];
            try
            {
                for (int i = 0; i < objQuadViewModel.EmpUniqueGoalID.Length; i++)
                {
                    int ID = objQuadViewModel.EmpUniqueGoalID[i];
                    cEmpGoal objEmpGoal = cEmpGoal.Get_ID(ID);
                      objEmpGoal.sManagerDescription = objQuadViewModel.ManagerComments[i];                                 
                      objEmpGoal.Save();                      
                }
                Session["ReviewQuadManagerComment"] = "Manager Comment added successfully";
              //  tempData.ReviewQuadManagerComment = "Manager Comment added successfully";
                objQuadrantMeasure.EmployeeModel = getEmployeeList();
                objQuadrantMeasure.StatusModel = getStatusList();
                //// string abc = getQuadrantName();

                DataTable dt = cQuadrantMeasure.reviewEmpQuadratants(empID);
                List<lstEmpQuadrantList> list = new List<lstEmpQuadrantList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new lstEmpQuadrantList { GoalName = dt.Rows[i]["GoalName"].ToString(), Measures = dt.Rows[i]["Measures"].ToString(), Count = i + 1, GoalId = Convert.ToInt32(dt.Rows[i]["GoalId"].ToString()), StatusName = dt.Rows[i]["StatusName"].ToString(), empcomment = dt.Rows[i]["empcomment"].ToString(), EmpGoalID = Convert.ToInt32(dt.Rows[i]["EmpGoalID"].ToString()), ManagerComment = dt.Rows[i]["ManagerComment"].ToString(), ManagerFlag = Convert.ToBoolean(dt.Rows[i]["ManagerFlag"].ToString()) });
                }
                objQuadrantMeasure.QuadList = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("ReviewQuadrantDetails", empID);
        }
        public ActionResult ReviewRatings()
        {
            try
            {
                objQuadrantMeasure.RatingModel = getRatingList();
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt1 = cQuadrantMeasure.getReviewRatingData(LoginId);
                if (dt1.Rows.Count > 0)
                {
                    objQuadrantMeasure.MgrComment = dt1.Rows[0]["ManagerComment"].ToString();
                    objQuadrantMeasure.ratingId = Convert.ToInt32(dt1.Rows[0]["RatingID"]);
                    objQuadrantMeasure.EmpAcceptance = dt1.Rows[0]["EmpAcceptance"].ToString();
                    objQuadrantMeasure.EmpFlag = Convert.ToBoolean(dt1.Rows[0]["EmpFlag"]);
                }
                DataTable dt = cQuadrantMeasure.getEmployeeManagerName(LoginId);
                objQuadrantMeasure.ManagerName = dt.Rows[0]["ManagerName"].ToString();
                objQuadrantMeasure.EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        public ActionResult ManagersComment()
        {
            try
            {
                objQuadrantMeasure.RatingModel = getRatingList();
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cQuadrantMeasure.getEmployeeManagerName(LoginId);
                objQuadrantMeasure.ManagerName = dt.Rows[0]["ManagerName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult ManagersComment(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                cReviewRating objReviewRating = cReviewRating.Create();
                objReviewRating.sManagerComment = objQuadViewModel.objReviewRating.sManagerComment;
                objReviewRating.sManagerSignature = objQuadViewModel.ManagerName;
                objReviewRating.objRatings.iObjectID = objQuadViewModel.objReviewRating.objRatings;
                objReviewRating.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                objReviewRating.Save();
                ViewBag.DataSaved = "External head added successfully";
                //objQuadrantMeasure.RatingModel = getRatingList();
                //int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                //DataTable dt = cQuadrantMeasure.getEmployeeManagerName(LoginId);
                //objQuadrantMeasure.ManagerName = dt.Rows[0]["ManagerName"].ToString();    
                // return View(objQuadrantMeasure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ReviewRatings");
        }
        public ActionResult EmployeeAcceptance()
        {
            try
            {
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cQuadrantMeasure.getEmployeeManagerName(LoginId);
                if (dt.Rows.Count > 0)
                {
                    objQuadrantMeasure.EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult EmployeeAcceptance(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                string empAcceptance = objQuadViewModel.objReviewRating.sEmployeeAcceptance;
                string empSignature = objQuadViewModel.EmployeeName;
                string abc = cQuadrantMeasure.updateEmployeeAcceptance(LoginId, empAcceptance, empSignature);
                Session["empAcceptanceSave"] = "Employee Acceptance added successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ReviewRatings");
        }
        public ActionResult EmployeeAcceptanceSubmit(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<cReviewRating> aob = cReviewRating.Find(" objEmpLogin = " + LoginID);
                if (aob.Count > 0)
                {
                    foreach (var item in aob)
                    {
                        item.bEmpFlag = true;
                        item.Save();
                    }
                    Session["empAcceptanceSubmit"] = "Employee Acceptance submitted successfully.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ReviewRatings");
        }
        public ActionResult DevelopmentGoals()
        {
            try
            {
                objQuadrantMeasure.StatusModel = getStatusList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult DevelopmentPlan(List<HttpPostedFileBase> file)
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                //FIle Upload:-

                if (file.Count > 0)
                {
                    foreach (var item in file)
                    {
                        if (item != null)
                        {
                            cFile objfile = cFile.Create();
                            string filename = Path.GetFileNameWithoutExtension(item.FileName) + DateTime.Now.ToString().Replace('/', '_').Replace(':', '_') + Path.GetExtension(item.FileName);
                            item.SaveAs(Server.MapPath("~/File/" + filename));
                            objfile.sFileName = filename;
                            objfile.objEmpLogin.iObjectID = loginID;
                            objfile.objCareerDevelopmentPlan.iObjectID = 0;
                            objfile.Save();
                        }

                    }
                }

                string DevopmentPlan = Request.QueryString["DevelopmentPlan"];
                List<Data> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data>>(DevopmentPlan);
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
                for (var i = 0; i < list.Count; i++)
                {
                    cCareerDevelopmentPlan objDevPlan = cCareerDevelopmentPlan.Create();

                    objDevPlan.sDevGoalName = list[i].GoalName;
                    objDevPlan.sActionRequired = list[i].Action;
                    objDevPlan.sTracking = list[i].Tracking;
                    objDevPlan.sManagerComment = list[i].ManagerComment;
                    objDevPlan.objStatus.iObjectID = list[i].StatusList;
                    objDevPlan.objEmpLogin.iObjectID = LoginId;
                    objDevPlan.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("DevelopmentGoals", "QuadrantMeasures");
            //return View(objQuadrantMeasure);
        }
        public ActionResult SummaryComments()
        {

            return View();
        }
        public void SaveSummaryComments()
        {
            try
            {
                string Summarys = Request.QueryString["SummaryComments"];
                Summarys = Summarys.Substring(1, Summarys.Length - 1);
                string[] strSummaryList = Summarys.Split(new[] { ',' });
                int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);

                for (int i = 0; i < strSummaryList.Length; i++)
                {
                    cSummaryComments objSummaryComments = cSummaryComments.Create();
                    objSummaryComments.sDescription = strSummaryList[i];
                    objSummaryComments.objEmpLogin.iObjectID = LoginId;
                    objSummaryComments.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getStatusData()
        {
            string json;
            try
            {
                JsonResult result = new JsonResult();
                SelectList StatusModelList = getStatusList();
                json = Newtonsoft.Json.JsonConvert.SerializeObject(StatusModelList);
                //  var jsonResult=  Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return json;
        }
        public ActionResult EmployeeQuadrants()
        {
            try
            {
                objQuadrantMeasure.EmployeeModel = getEmployeeList();
                objQuadrantMeasure.StatusModel = getStatusList();
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);

                List<lstQuadrantList> list = new List<lstQuadrantList>();
                List<cEmpGoal> aobj = cEmpGoal.Find(" objEmpLogin = " + LoginID);
                if (aobj.Count > 0)
                {
                    int count = 0;
                    foreach (var item in aobj)
                    {
                        count++;
                        list.Add(new lstQuadrantList { Count = count, GoalId = item.objGoals.iObjectID, Measures = item.sMesaures, EmpGoalID = item.iID, empcomment = item.sDescription, GoalName = cGoals.Get_ID(Convert.ToInt32(item.objGoals.iObjectID)).sName, EmpFlag = item.bEmpFlag, ManagerFlag = item.bManagerFlag, ManagerComment = item.sManagerDescription });
                    }
                    objQuadrantMeasure.list1 = list;
                }
                else
                {
                    DataTable dt = cQuadrantMeasure.getEmpQuadratants(LoginID);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            list.Add(new lstQuadrantList { GoalName = dt.Rows[i]["GoalName"].ToString(), Measures = dt.Rows[i]["Measures"].ToString(), Count = i + 1, GoalId = Convert.ToInt32(dt.Rows[i]["GoalId"].ToString()) });
                        }
                    }
                    objQuadrantMeasure.list1 = list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult EmployeeQuadrants(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                for (int i = 0; i < objQuadViewModel.GoalID.Length; i++)
                {
                    cEmpGoal objEmpGoals = cEmpGoal.Create();
                    // objEmpGoals.objStatus.iObjectID = objQuadViewModel.goalStatusID[i];
                    objEmpGoals.objGoals.iObjectID = Convert.ToInt32(objQuadViewModel.GoalID[i]);
                    objEmpGoals.sMesaures = objQuadViewModel.Measures[i];
                    objEmpGoals.sDescription = objQuadViewModel.empComments[i];
                    objEmpGoals.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    objEmpGoals.Save();                    
                }
                Session["EmpQudSave"] = "Employee Quadrants added successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("EmployeeQuadrants");
        }
        [HttpPost]
        public ActionResult EmployeeQuadrantsSubmit(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<cEmpGoal> aob = cEmpGoal.Find(" objEmpLogin = " + LoginID);
                if (aob.Count > 0)
                {
                    foreach (var item in aob)
                    {
                        item.bEmpFlag = true;
                        item.Save();
                    }
                    Session["EmpQuadSubmit"] = "Employee Quadrants submitted successfully.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("EmployeeQuadrants");
        }
        [HttpPost]
        public ActionResult EmployeeQuadrantsUpdate(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                for (int i = 0; i < objQuadViewModel.EmployeeGoalID.Length; i++)
                {
                    cEmpGoal objEmpGoals = cEmpGoal.Get_ID(Convert.ToInt32(objQuadViewModel.EmployeeGoalID[i]));
                    objEmpGoals.sDescription = objQuadViewModel.empComments[i];
                    objEmpGoals.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    objEmpGoals.Save();
                }
                Session["EmpQuadUpdate"] = "Employee Quadrants updated successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("EmployeeQuadrants");
        }
        [HttpPost]
        public ActionResult EmployeeReviewQuadrantsUpdate(QuadrantMeasuresViewModel objQuadViewModel)
        {
            for (int i = 0; i < objQuadViewModel.EmpUniqueGoalID.Length; i++)
            {
                cEmpGoal objEmpGoals = cEmpGoal.Get_ID(Convert.ToInt32(objQuadViewModel.EmpUniqueGoalID[i]));
                objEmpGoals.sManagerDescription = objQuadViewModel.ManagerComments[i];
                // objEmpGoals.objEmpLogin.iObjectID = objQuadViewModel.hdnEmployeeID;
                objEmpGoals.Save();               
            }
            Session["ReviewQuadUpdateManagerComment"] = "Manager Comment updated successfully";
            int empID = Convert.ToInt32(objQuadViewModel.hdnEmployeeID);
            return RedirectToAction("ReviewQuadrantDetails/" + empID);
        }
        [HttpPost]
        public ActionResult EmployeeReviewQuadrantsSubmit(QuadrantMeasuresViewModel objQuadViewModel)
        {
            int ID = objQuadViewModel.hdnEmployeeID;
            List<cEmpGoal> aob = cEmpGoal.Find(" objEmpLogin = " + ID);
            if (aob.Count > 0)
            {
                foreach (var item in aob)
                {
                    item.bManagerFlag = true;
                    item.Save();                    
                }
                Session["ReviewQuadSubmitManagerComment"] = "Manager Comment submitted successfully";
            }
            return RedirectToAction("ReviewQuadrantDetails/" + objQuadViewModel.hdnEmployeeID);
        }
        public ActionResult Performace()
        {
            return View();
        }
        public ActionResult ReviewDevelopmentGoal(int ID)
        {
            try
            {
                List<FileAttachment> listFile = new List<FileAttachment>();

                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);

                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(ID);
                int intReportingHead = objEmpLogin.objEmpLogin.iObjectID;

                cEmpLogin objemp = cEmpLogin.Get_ID(LoginID);
                string RoleAccessName = cRoleAccess.Get_ID(objemp.objRoleAccess.iObjectID).sName.ToString();

                if (RoleAccessName != "Admin")
                {
                    if (intReportingHead != LoginID)
                    {
                        return RedirectToAction("SignOut", "Login");
                    }
                }

                if (RoleAccessName == "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.ReportingHeadFlag = true;
                    objQuadrantMeasure.AdminFlag = true;
                }
                else if (RoleAccessName == "Admin" && intReportingHead != LoginID)
                {

                    objQuadrantMeasure.AdminFlag = true;
                    objQuadrantMeasure.ReportingHeadFlag = false;
                }
                else if (RoleAccessName != "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.AdminFlag = false;
                    objQuadrantMeasure.ReportingHeadFlag = true;
                }

                objQuadrantMeasure.StatusModel = getStatusList();
                // int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cQuadrantMeasure.getEmpDevelopmentGoals(ID);
                List<lstDevelopmentGoalList> list = new List<lstDevelopmentGoalList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new lstDevelopmentGoalList { GoalName = dt.Rows[i]["GoalName"].ToString(), ActionRequired = dt.Rows[i]["ActionRequired"].ToString(), Tracking = dt.Rows[i]["Tracking"].ToString(), Count = i + 1, StatusName = dt.Rows[i]["StatusID"].ToString(), ID = Convert.ToInt32(dt.Rows[i]["DevID"].ToString()), ManagerComment = dt.Rows[i]["ManagerComment"].ToString() });
                }
                objQuadrantMeasure.list2 = list;




                //" objEmpLogin = " + ID
                //For Get File List:
                List<cFile> aobFile = cFile.Find();
                List<FileAttachment> aobfile = new List<FileAttachment>();
                if (aobFile.Count > 0)
                {
                    foreach (var item in aobFile)
                    {
                        string fileTitle = Path.GetFileNameWithoutExtension(item.sFileName);
                        aobfile.Add(new FileAttachment { FileName = fileTitle, ID = item.iID.ToString(), Extension = Path.GetExtension(item.sFileName) });
                    }

                }
                objQuadrantMeasure.listFileAttachment = aobfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        [HttpPost]
        public ActionResult ReviewDevelopmentGoal(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                for (int i = 0; i < objQuadViewModel.DevelopmentPlanID.Length; i++)
                {
                    int ID = objQuadViewModel.DevelopmentPlanID[i];
                    cCareerDevelopmentPlan objDevelopmentPlan = cCareerDevelopmentPlan.Get_ID(ID);
                    objDevelopmentPlan.sManagerComment = objQuadViewModel.ManagerComments[i];
                    objDevelopmentPlan.Save();
                }
                Session["ReviewDevGoal"] = "Manager Comment added successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ReviewDevelopmentGoal");
        }
        public class Data
        {
            public string GoalName { get; set; }
            public string Action { get; set; }
            public string Tracking { get; set; }
            public string ManagerComment { get; set; }
            public int StatusList { get; set; }
        }
        public ActionResult ReviewSummaryComments(int ID)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);

                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(ID);
                int intReportingHead = objEmpLogin.objEmpLogin.iObjectID;

                cEmpLogin objemp = cEmpLogin.Get_ID(LoginID);
                string RoleAccessName = cRoleAccess.Get_ID(objemp.objRoleAccess.iObjectID).sName.ToString();

                if (RoleAccessName != "Admin")
                {
                    if (intReportingHead != LoginID)
                    {
                        return RedirectToAction("SignOut", "Login");
                    }
                }


                // int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cQuadrantMeasure.getEmpSummaryComments(ID);
                List<lstSummaryComments> list = new List<lstSummaryComments>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new lstSummaryComments { SummaryComments = dt.Rows[i]["SummaryComments"].ToString() });
                }
                objQuadrantMeasure.list3 = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        //Added Swpnesh:-
        [HttpGet]
        public ActionResult AddQuadrants()
        {
            AddQuadrant quadrant = new AddQuadrant();
            try
            {

                quadrant.QuadrantDepartmentTypeList = getDepartmentTypeList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(quadrant);
        }
        [HttpPost]
        public ActionResult AddQuadrants(AddQuadrant quadrant)
        {
            try
            {
                cGoals goal = cGoals.Create();
                goal.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                goal.objDepartmentType.iObjectID = Convert.ToInt32(quadrant.SelectedQuadrantDepartmentType[0]);
                goal.sName = quadrant.QuadrantName;
                goal.bIsActive = true;
                goal.Save();
                quadrant.QuadrantDepartmentTypeList = getDepartmentTypeList();
                ViewBag.DataSaved = "Goal added successfully";
                return View(quadrant);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private SelectList getDepartmentTypeList()
        {
            try
            {
                List<ddlDepartmentTypeList> objDeptTypeList = new List<ddlDepartmentTypeList>();
                List<cDepartmentType> objDeptType = cDepartmentType.Find();

                objDeptTypeList.Add(new ddlDepartmentTypeList { Value = 0, Text = "--select--" });
                foreach (var item in objDeptType)
                {
                    objDeptTypeList.Add(new ddlDepartmentTypeList { Value = item.iID, Text = item.sName });
                }

                SelectList objDeptTypeListData = new SelectList(objDeptTypeList, "Value", "Text");
                return objDeptTypeListData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ManagerReviewRating(int ID)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);

                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(ID);
                int intReportingHead = objEmpLogin.objEmpLogin.iObjectID;

                cEmpLogin objemp = cEmpLogin.Get_ID(LoginID);
                string RoleAccessName = cRoleAccess.Get_ID(objemp.objRoleAccess.iObjectID).sName.ToString();

                if (RoleAccessName != "Admin")
                {
                    if (intReportingHead != LoginID)
                    {
                        return RedirectToAction("SignOut", "Login");
                    }
                }

                if (RoleAccessName == "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.ReportingHeadFlag = true;
                    objQuadrantMeasure.AdminFlag = true;
                }
                else if (RoleAccessName == "Admin" && intReportingHead != LoginID)
                {

                    objQuadrantMeasure.AdminFlag = true;
                    objQuadrantMeasure.ReportingHeadFlag = false;
                }
                else if (RoleAccessName != "Admin" && intReportingHead == LoginID)
                {
                    objQuadrantMeasure.AdminFlag = false;
                    objQuadrantMeasure.ReportingHeadFlag = true;
                }

                objQuadrantMeasure.RatingModel = getRatingList();
                objQuadrantMeasure.hdnEmployeeID = ID;
                DataTable dt1 = cQuadrantMeasure.getReviewRatingData(ID);

                List<cReviewRating> aob = cReviewRating.Find(" objEmpLogin = " + ID);
                if (aob.Count > 0)
                {
                    objQuadrantMeasure.MgrComment = aob[0].sManagerComment;
                    objQuadrantMeasure.ratingId = Convert.ToInt32(aob[0].objRatings.iObjectID);
                    objQuadrantMeasure.MangerFlag = aob[0].bManagerFlag;
                }
                List<cReviewRating> aob1 = cReviewRating.Find(" objEmpLogin = " + ID + " and bEmpFlag = " + true);
                if (aob1.Count > 0)
                {
                    objQuadrantMeasure.EmpAcceptance = aob[0].sEmployeeAcceptance;
                }
                else
                {
                    objQuadrantMeasure.EmpAcceptance = "";
                }
                if (aob.Count > 0)
                {
                    objQuadrantMeasure.MgrComment = aob[0].sManagerComment;
                    objQuadrantMeasure.ratingId = Convert.ToInt32(aob[0].objRatings.iObjectID);
                    objQuadrantMeasure.MangerFlag = aob[0].bManagerFlag;
                }

                DataTable dt = cQuadrantMeasure.getEmployeeManagerName(ID);
                if (dt.Rows.Count > 0)
                {
                    objQuadrantMeasure.ManagerName = dt.Rows[0]["ManagerName"].ToString();
                    objQuadrantMeasure.EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadrantMeasure);
        }
        public ActionResult ReviewManagersComment(int ID)
        {
            QuadrantMeasuresViewModel objQuadViewModel = new QuadrantMeasuresViewModel();
            try
            {
                objQuadViewModel.hdnEmployeeID = ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objQuadViewModel);

        }
        [HttpPost]
        public ActionResult ReviewManagersComment(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                cReviewRating objReviewRating = cReviewRating.Create();
                objReviewRating.sManagerComment = objQuadViewModel.objReviewRating.sManagerComment;
                objReviewRating.sManagerSignature = objQuadViewModel.ManagerName;
                objReviewRating.objRatings.iObjectID = objQuadViewModel.objReviewRating.objRatings;
                objReviewRating.objEmpLogin.iObjectID = objQuadViewModel.hdnEmployeeID;
                objReviewRating.Save();
                Session["RevMgrComment"] = "Manager Comment added successfully";
                objQuadrantMeasure.RatingModel = getRatingList();
                DataTable dt = cQuadrantMeasure.getEmployeeManagerName(objQuadViewModel.hdnEmployeeID);
                objQuadrantMeasure.ManagerName = dt.Rows[0]["ManagerName"].ToString();
                objQuadrantMeasure.EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ManagerReviewRating/" + objQuadViewModel.hdnEmployeeID);
            //  return View(objQuadrantMeasure);
            //return RedirectToAction("ManagerReviewRating");

            //return RedirectToAction("ManagerReviewRating", objQuadViewModel.hdnEmployeeID);
        }
        [HttpPost]
        public ActionResult UpdateManagerComment(QuadrantMeasuresViewModel objQuadViewModel)
        {
            int ID = objQuadViewModel.hdnEmployeeID;
            try
            {

                List<cReviewRating> aob = cReviewRating.Find(" objEmpLogin = " + objQuadViewModel.hdnEmployeeID);
                foreach (var item in aob)
                {
                    item.sManagerComment = objQuadViewModel.objReviewRating.sManagerComment;
                    item.sManagerSignature = objQuadViewModel.ManagerName;
                    item.objRatings.iObjectID = objQuadViewModel.objReviewRating.objRatings;
                    item.objEmpLogin.iObjectID = objQuadViewModel.hdnEmployeeID;
                    item.Save();
                    Session["RevMgrUpdateComment"] = "Manager Comment updated successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return RedirectToAction("ManagerReviewRating/" + ID);
        }
        [HttpPost]
        public ActionResult UpdateEmployeeAcceptance(QuadrantMeasuresViewModel objQuadViewModel)
        {
            int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
            try
            {

                List<cReviewRating> aob = cReviewRating.Find(" objEmpLogin = " + loginID);
                foreach (var item in aob)
                {
                    item.sEmployeeAcceptance = objQuadViewModel.objReviewRating.sEmployeeAcceptance;
                    item.Save();
                }
                Session["empAcceptanceUpdate"] = "Employee Acceptance updated successfully.";

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return RedirectToAction("ReviewRatings");
        }
        public ActionResult ReviewEmployeeAcceptance()
        {
            return View();
        }
        public ActionResult ReviewManagerCommentSubmit(QuadrantMeasuresViewModel objQuadViewModel)
        {
            try
            {
                string update = cEmpGoal.updateManagerFlag(objQuadViewModel.hdnEmployeeID);
                List<cReviewRating> aob = cReviewRating.Find(" objEmpLogin = " + objQuadViewModel.hdnEmployeeID);

                foreach (var item in aob)
                {
                    item.bManagerFlag = true;
                    item.Save();
                }
                Session["RevMgrSubmitComment"] = "Manager Comment submitted successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("ManagerReviewRating/" + objQuadViewModel.hdnEmployeeID);
        }
        public FileResult Download(string id)
        {

            try
            {
                int fid = Convert.ToInt32(id);
                cFile objFile = cFile.Get_ID(fid);
                if (!String.IsNullOrEmpty(objFile.sFileName))
                {
                    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + objFile.sFileName);
                }
                string path = "~/File/" + objFile.sFileName;
                HttpContext.Response.TransmitFile(path);
                return File(path, System.Net.Mime.MediaTypeNames.Application.Octet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //List & Delete Quadrant Measures:-
        public ActionResult QuadrantsMeasureList()
        {
            int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
            List<cQuadrantMeasure> aobjQuadrant = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID);
            List<QuadrantMeasureList> lstQuadrant = new List<QuadrantMeasureList>();

            if (aobjQuadrant.Count > 0)
            {
                foreach (var item in aobjQuadrant)
                {
                    lstQuadrant.Add(new QuadrantMeasureList { QuadrantID = item.iID.ToString(), DepartmentName = cDepartmentType.Get_ID(Convert.ToInt32(item.objDepartmentType.iObjectID)).sName, QuadrantName = cGoals.Get_ID(Convert.ToInt32(item.objGoals.iObjectID)).sName });
                }
            }

            return View(lstQuadrant);
        }
        public JsonResult QuadrantDelete(string ID)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                int Id = Convert.ToInt32(ID);
                List<cQuadrantMeasure> aobQaud = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID + " and iID = " + Id);
                if (aobQaud.Count > 0)
                {

                    cQuadrantMeasure.Delete(Id);
                }
                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //List For CareerDevelopmentPlan
        public ActionResult DevelopmentGoalList()
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<cCareerDevelopmentPlan> aob = cCareerDevelopmentPlan.Find(" objEmpLogin = " + loginID);
                List<DevelopmentGoalList> objGoalList = new List<DevelopmentGoalList>();
                if (aob.Count > 0)
                {
                    foreach (var item in aob)
                    {
                        objGoalList.Add(new DevelopmentGoalList { GoalName = item.sDevGoalName, Action = item.sActionRequired, Tracking = item.sTracking, ManagerComment = item.sManagerComment });
                    }

                }
                return View(objGoalList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SummaryCommentList()
        {

            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<cSummaryComments> aob = cSummaryComments.Find(" objEmpLogin = " + loginID);
                List<SummaryCommentList> objSummaryCommentList = new List<SummaryCommentList>();
                if (aob.Count > 0)
                {
                    foreach (var item in aob)
                    {
                        objSummaryCommentList.Add(new SummaryCommentList { SummaryComment = item.sDescription });
                    }

                }
                return View(objSummaryCommentList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}