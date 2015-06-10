using AquatroHRIMS.App_Code;
using AquatroHRIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRIMS;
using AquatroHRIMS.ViewModel;
using Newtonsoft.Json;
using System.Data;
using AquatroHRIMS.ActionFilters;
using System.Web.Script.Serialization;

namespace AquatroHRIMS.Controllers
{
    [CustomException]
    public class DashBoardController : Controller
    {

        //
        // GET: /DashBoard/
        public ActionResult Index()
        {
            try
            {
                List<cDailyQuotes> objDailyQuet=cDailyQuotes.Find();
                DashBoardView objDashView = new DashBoardView();
                objDashView.objNews = GetNews();
                objDashView.objAssignProject = GetAssignProject();
                objDashView.objBirthDay = GetBirthday();
                objDashView.objNewJoinee = GetNewJoinee();
                objDashView.objPendingLeave = GetPendingLeave();
                if (objDailyQuet.Count > 0)
                {
                    objDashView.DailyQuates = objDailyQuet[0].sName;
                }
                else {

                    objDashView.DailyQuates = "";
                }
               
                return View(objDashView);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public ActionResult Profile()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                string EmergancyContact = "";
                string ContactNo = "";
                string Relation = "";

                ProfileViewModel objProfile = new ProfileViewModel();

                cEmpLogin empLogin = cEmpLogin.Get_ID(LoginID);
                objProfile.objEmpPersonal = new EmployeePersonalDetails
                {
                    WorkEmail = empLogin.sEmailID
                };

                //-------  Employee Personal Detail data check----//
                List<cEmpEmergencyContact> emerobj = cEmpEmergencyContact.Find(" objEmpLogin = " + empLogin.iID);
                if (emerobj.Count > 0)
                {
                    EmergancyContact = emerobj[0].sName;
                    ContactNo = emerobj[0].sContactNo;
                    Relation = emerobj[0].sRelationShip;
                }
                else
                {

                }
                List<cEmpPersonalDetails> aobjPerDetails = cEmpPersonalDetails.Find(" objEmpLogin = " + empLogin.iID);
                if (aobjPerDetails.Count > 0)
                {

                    if (aobjPerDetails.Count > 0)
                    {
                        objProfile.objEmpPersonal = new EmployeePersonalDetails
                        {
                            FirstName = aobjPerDetails[0].sFirstName,
                            MiddleName = aobjPerDetails[0].sMiddleName,
                            LastName = aobjPerDetails[0].sLastName,
                            MotherName = aobjPerDetails[0].sMotherName,
                            Gender = aobjPerDetails[0].sGender,
                            DOB = aobjPerDetails[0].dtDOB,
                            CurrentAddress = aobjPerDetails[0].sCurrAddress,
                            PermanentAddress = aobjPerDetails[0].sPerAddress,
                            MartialStatus = aobjPerDetails[0].sMartialStatus,
                            Country = aobjPerDetails[0].sCountry,
                            City = aobjPerDetails[0].sCity,
                            State = aobjPerDetails[0].sState,
                            ZipCode = aobjPerDetails[0].sZipCode,
                            Nationality = aobjPerDetails[0].sNationality,
                            PersonalContactNo = aobjPerDetails[0].sPerContactNo,
                            WorkContact = aobjPerDetails[0].sWorkTelp,
                            GovID = aobjPerDetails[0].sGovtID,
                            PersonalEmail = aobjPerDetails[0].sPersoanlEmailID,
                            WorkEmail = cEmpLogin.Get_ID(LoginID).sEmailID,

                            EmergancyContactName = EmergancyContact,
                            ContactNo = ContactNo,
                            RelationShip = Relation
                        };
                    }

                }


                //-------- Employee Emergency details---------//




                //----------Employee Education Details Check---------//

                List<cEmpEducationDetail> objEducation = cEmpEducationDetail.Find(" objEmpLogin = " + empLogin.iID);
                if (objEducation.Count > 0)
                {
                    objProfile.objEmpEducation = new EmpEducationDetails
                    {
                        qualification = objEducation[0].sHigestDegree,
                        Specialization = objEducation[0].sSpecialization,
                        PassingYear = objEducation[0].sPassingYear,
                        InstituteName = objEducation[0].sInstituteName,
                        Percentage = objEducation[0].fPercentage,

                        HighlySecqualification = objEducation[0].sHigestDegree,
                        HighlySecSpecialization = objEducation[0].sSpecialization,
                        HighlySecPassingYear = objEducation[0].sPassingYear,
                        HighlySecInstituteName = objEducation[0].sInstituteName,
                        HighlySecPercentage = objEducation[0].fPercentage,

                        Secqualification = objEducation[0].sHigestDegree,
                        SecPassingYear = objEducation[0].sPassingYear,
                        SecInstituteName = objEducation[0].sInstituteName,
                        SecPercentage = objEducation[0].fPercentage,
                    };
                }
                else
                {
                    objProfile.objEmpEducation = new EmpEducationDetails();
                }

                //--------Employee Professional detail---------//

                List<cEmpProfessionalDetail> objProfessional = cEmpProfessionalDetail.Find(" objEmpLogin = " + empLogin.iID);
                if (objProfessional.Count > 0)
                {
                    objProfile.objEmpProfessional = new EmpProfessionalDetails
                    {
                        CompanyName = objProfessional[0].sCompanyName,
                        Designation = objProfessional[0].sDesignation,
                        FromDate = objProfessional[0].dtFromDate,
                        EndDate = objProfessional[0].dtEndDate,
                    };
                }
                else
                {
                    objProfile.objEmpProfessional = new EmpProfessionalDetails();
                }

                return View(objProfile);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            //objPers.objEmpPersonal.FirstName=aobjEmployeePersonalDetails[0].sFirstName.ToString();
            //objPers.objEmpPersonal.MiddleName = aobjEmployeePersonalDetails[0].sMiddleName.ToString(); ;
            //objPers.objEmpPersonal.LastName = aobjEmployeePersonalDetails[0].sLastName.ToString() ;
            //objPers.PersonalEmail = aobjEmployeePersonalDetails[0].sPersoanlEmailID.ToString();
            //objPers.objEmpPersonal.Gender = aobjEmployeePersonalDetails[0].sGender.ToString();
            //objPers.WorkEmail = objEmpLogin.sEmailID.ToString();

        }
        [HttpPost]
        public JsonResult personaldeatil(EmployeePersonalDetails objEmpPersonal)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);

                cEmpLogin objEmpLogin = cEmpLogin.Get_ID(LoginID);
                List<cEmpPersonalDetails> aobjEmployeePersonalDetails = cEmpPersonalDetails.Find(" objEmpLogin = " + objEmpLogin.iID);
                if (aobjEmployeePersonalDetails.Count > 0)
                {

                    aobjEmployeePersonalDetails[0].sFirstName = objEmpPersonal.FirstName;
                    aobjEmployeePersonalDetails[0].sMiddleName = objEmpPersonal.MiddleName;
                    aobjEmployeePersonalDetails[0].sLastName = objEmpPersonal.LastName;
                    aobjEmployeePersonalDetails[0].sPersoanlEmailID = objEmpPersonal.PersonalEmail;
                    aobjEmployeePersonalDetails[0].sGender = objEmpPersonal.Gender;
                    // objEmployeePersonalDetails.sFatherName = objprofilemodel.objEmpPersonal.FatherName;
                    aobjEmployeePersonalDetails[0].sMotherName = objEmpPersonal.MotherName;
                    aobjEmployeePersonalDetails[0].sCurrAddress = objEmpPersonal.CurrentAddress;
                    aobjEmployeePersonalDetails[0].sPerAddress = objEmpPersonal.PermanentAddress;
                    aobjEmployeePersonalDetails[0].sMartialStatus = objEmpPersonal.MartialStatus;
                    aobjEmployeePersonalDetails[0].dtDOB = objEmpPersonal.DOB;
                    aobjEmployeePersonalDetails[0].sGovtID = objEmpPersonal.GovID;
                    aobjEmployeePersonalDetails[0].sCity = objEmpPersonal.City;
                    aobjEmployeePersonalDetails[0].sState = objEmpPersonal.State;
                    aobjEmployeePersonalDetails[0].sZipCode = objEmpPersonal.ZipCode;
                    aobjEmployeePersonalDetails[0].sCountry = objEmpPersonal.Country;
                    aobjEmployeePersonalDetails[0].sNationality = objEmpPersonal.Nationality;
                    aobjEmployeePersonalDetails[0].sWorkTelp = objEmpPersonal.WorkContact;
                    aobjEmployeePersonalDetails[0].objEmpLogin.iObjectID = LoginID;
                  
                    aobjEmployeePersonalDetails[0].Save();
                    objEmpLogin.sFirstTime = "2";
                    objEmpLogin.Save();
                }

                List<cEmpEmergencyContact> aobjEmpEmergency = cEmpEmergencyContact.Find(" objEmpLogin = " + objEmpLogin.iID);


                if (aobjEmpEmergency.Count > 0)
                {
                    aobjEmpEmergency[0].sName = objEmpPersonal.EmergancyContactName;
                    aobjEmpEmergency[0].sContactNo = objEmpPersonal.ContactNo;
                    aobjEmpEmergency[0].sRelationShip = objEmpPersonal.RelationShip;
                    aobjEmpEmergency[0].objEmpLogin.iObjectID = LoginID;
                    aobjEmpEmergency[0].Save();

                }
                else
                {
                    cEmpEmergencyContact aoEmergencycreate = cEmpEmergencyContact.Create();
                    aoEmergencycreate.sName = objEmpPersonal.EmergancyContactName;
                    aoEmergencycreate.sContactNo = objEmpPersonal.ContactNo;
                    aoEmergencycreate.sRelationShip = objEmpPersonal.RelationShip;
                    aoEmergencycreate.objEmpLogin.iObjectID = LoginID;
                    aoEmergencycreate.Save();
                }
                objEmpLogin.sFirstTime = "2";
                return Json(aobjEmployeePersonalDetails[0]);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public ActionResult EducationDetail(EmpEducationDetails objEmpEducation)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<cEmpEducationDetail> objeducation = cEmpEducationDetail.Find(" objEmpLogin = " + LoginID);
                if (objeducation == null)
                {
                    cEmpEducationDetail objCreateEdu = cEmpEducationDetail.Create();
                    objCreateEdu.sInstituteName = objEmpEducation.InstituteName;
                    objCreateEdu.sHigestDegree = objEmpEducation.qualification;
                    objCreateEdu.sSpecialization = objEmpEducation.Specialization;
                    objCreateEdu.sPassingYear = objEmpEducation.PassingYear;
                    objCreateEdu.fPercentage = (float)Convert.ToDouble(objEmpEducation.Percentage);
                    objCreateEdu.objEmpLogin.iObjectID = LoginID;
                    objCreateEdu.Save();

                    cEmpEducationDetail objhighEdu = cEmpEducationDetail.Create();
                    objhighEdu.sInstituteName = objEmpEducation.HighlySecInstituteName;
                    objhighEdu.sHigestDegree = objEmpEducation.HighlySecqualification;
                    objhighEdu.sSpecialization = objEmpEducation.HighlySecSpecialization;
                    objhighEdu.sPassingYear = objEmpEducation.HighlySecPassingYear;
                    objhighEdu.fPercentage = (float)Convert.ToDouble(objEmpEducation.HighlySecPercentage);
                    objhighEdu.objEmpLogin.iObjectID = LoginID;
                    objhighEdu.Save();

                    cEmpEducationDetail objSecondaryEdu = cEmpEducationDetail.Create();
                    objSecondaryEdu.sInstituteName = objEmpEducation.SecInstituteName;
                    objSecondaryEdu.sHigestDegree = objEmpEducation.Secqualification;
                    objSecondaryEdu.sPassingYear = objEmpEducation.SecPassingYear;
                    objSecondaryEdu.fPercentage = (float)Convert.ToDouble(objEmpEducation.SecPercentage);
                    objSecondaryEdu.objEmpLogin.iObjectID = LoginID;
                    objSecondaryEdu.Save();
                }
                else
                {
                    objeducation[0].sInstituteName = objEmpEducation.InstituteName;
                    objeducation[0].sHigestDegree = objEmpEducation.qualification;
                    objeducation[0].sSpecialization = objEmpEducation.Specialization;
                    objeducation[0].sPassingYear = objEmpEducation.PassingYear;
                    objeducation[0].fPercentage = (float)Convert.ToDouble(objEmpEducation.Percentage);
                    objeducation[0].objEmpLogin.iObjectID = LoginID;
                    objeducation[0].Save();


                    objeducation[1].sInstituteName = objEmpEducation.HighlySecInstituteName;
                    objeducation[1].sHigestDegree = objEmpEducation.HighlySecqualification;
                    objeducation[1].sSpecialization = objEmpEducation.HighlySecSpecialization;
                    objeducation[1].sPassingYear = objEmpEducation.HighlySecPassingYear;
                    objeducation[1].fPercentage = (float)Convert.ToDouble(objEmpEducation.HighlySecPercentage);
                    objeducation[1].objEmpLogin.iObjectID = LoginID;
                    objeducation[1].Save();


                    objeducation[2].sInstituteName = objEmpEducation.SecInstituteName;
                    objeducation[2].sHigestDegree = objEmpEducation.Secqualification;
                    objeducation[2].sPassingYear = objEmpEducation.SecPassingYear;
                    objeducation[2].fPercentage = (float)Convert.ToDouble(objEmpEducation.SecPercentage);
                    objeducation[2].objEmpLogin.iObjectID = LoginID;
                    objeducation[2].Save();
                }
                TempData["Prof"] = 1;
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        //[HttpPost]
        //public JsonResult ProfessionalDetails(EmpProfessionalDetails objEmpProfessional)
        //{
        //    try
        //    {
        //        int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
        //        cEmpProfessionalDetail objempProfessional = cEmpProfessionalDetail.Get_ID(LoginID);

        //        if (objempProfessional == null)
        //        {
        //            cEmpProfessionalDetail objEudCreate = cEmpProfessionalDetail.Create();
        //            objEudCreate.sCompanyName = objEmpProfessional.CompanyName;
        //            objEudCreate.sDesignation = objEmpProfessional.Designation;
        //            objempProfessional.sProfileDescription = objprofilemodel.objEmpProfessional.ProfileDescritpion;
        //            objEudCreate.dtFromDate = objEmpProfessional.FromDate;
        //            objEudCreate.dtEndDate = objEmpProfessional.EndDate;
        //            objEudCreate.objEmpLogin.iObjectID = LoginID;
        //            objEudCreate.Save();
        //        }
        //        else
        //        {
        //            objempProfessional.sCompanyName = objEmpProfessional.CompanyName;
        //            objempProfessional.sDesignation = objEmpProfessional.Designation;
        //            objempProfessional.sProfileDescription = objprofilemodel.objEmpProfessional.ProfileDescritpion;
        //            objempProfessional.dtFromDate = objEmpProfessional.FromDate;
        //            objempProfessional.dtEndDate = objEmpProfessional.EndDate;
        //            objempProfessional.objEmpLogin.iObjectID = LoginID;
        //            objempProfessional.Save();
        //        }
        //        return Json("1");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //Get News:-
        public List<NewsList> GetNews()
        {
            try
            {
          
                List<NewsList> objNews = new List<NewsList>();
                DataTable dt = cEmployee.GetNews();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objNews.Add(new NewsList() { Heading = dt.Rows[i]["sHeadLine"].ToString(), Description = dt.Rows[i]["sDescription"].ToString()});
                }
                return objNews;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Get Pending LeaveRequest:-
        public List<PendingLeaveList> GetPendingLeave()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                List<PendingLeaveList> objPendingleave = new List<PendingLeaveList>();
                List<cEmpLeave> aobEmpLeave = cEmpLeave.Find(" objEmpLogin = " + LoginID);

                foreach (var item in aobEmpLeave)
                {
                    objPendingleave.Add(new PendingLeaveList { LeaveName = item.sReason, Status = cStatus.Get_ID(Convert.ToInt32(item.sLeaveStatus)).sName });
                }
                return objPendingleave;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //Get Birthday List:-
        public List<BirthDayList> GetBirthday()
        {
            try
            {
                DataTable dt = cEmployee.GetEmpBirthDay();
                List<BirthDayList> objBirth = new List<BirthDayList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objBirth.Add(new BirthDayList() { Name = dt.Rows[i]["Name"].ToString(), DOB = dt.Rows[i]["DOB"].ToString(), });
                }

                return objBirth;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Get Joinee List:-
        public List<NewJoineeList> GetNewJoinee()
        {
            try
            {
                DataTable dt = cEmployee.GetNewJoinee();
                List<NewJoineeList> aobjemployee = new List<NewJoineeList>();
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        aobjemployee.Add(new NewJoineeList() { Name = dt.Rows[i]["Name"].ToString(), EmployeeCode = dt.Rows[i]["EmployeeCode"].ToString(), DepartmentType = dt.Rows[i]["DepartmentType"].ToString() });
                    }

                return aobjemployee;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AssignProject> GetAssignProject()
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DataTable dt = cEmployee.GetProject(LoginID);
                List<AssignProject> aobjproject = new List<AssignProject>();
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        aobjproject.Add(new AssignProject() { ProjectName = dt.Rows[i]["ProjectName"].ToString(), Description = dt.Rows[i]["sDescription"].ToString() });
                    }
                return aobjproject;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult GetHolidayList()
        {

            try
            {
                List<Event> events = new List<Event>();
                List<cHolidayList> aob = cHolidayList.Find();
                foreach (var item in aob)
                {
                    events.Add(new Event()
                    {

                        title = item.sOccassion,
                        start = item.dtOccDate.ToString("yyyy-MM-dd"),
                        description = item.sDescription,
                        backgroundColor = "red"
                    });
                }


                JavaScriptSerializer jss = new JavaScriptSerializer();

                string output = jss.Serialize(events);

                return Json(output);
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



        [HttpPost]
        public ActionResult EmpProfessional()
        {

            string Professional = Request.QueryString["Professional"];
            List<EmpProfessionalDetails> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpProfessionalDetails>>(Professional);
            int LoginId = Convert.ToInt32(HttpContext.User.Identity.Name);
            for (var i = 0; i < list.Count; i++)
            {
                cEmpProfessionalDetail objEudCreate = cEmpProfessionalDetail.Create();
                objEudCreate.sCompanyName = list[i].CompanyName;
                objEudCreate.sDesignation = list[i].Designation;
                DateTime dt1 = list[i].FromDate;
                DateTime dt2 = list[i].EndDate;
                objEudCreate.dtFromDate = Convert.ToDateTime(dt1);
                objEudCreate.dtEndDate = Convert.ToDateTime(dt2);
                objEudCreate.objEmpLogin.iObjectID = LoginId;
                objEudCreate.Save();
            }

            TempData["Prof"] = 1;

            return RedirectToAction("Profile");

        }
    }
}