using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AquatroHRIMS.Models;
using AquatroHRIMS.App_Code;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using HRIMS;

namespace AquatroHRIMS.Controllers
{
    public class LoginController : Controller
    {

        string url = ConfigurationManager.AppSettings["URL"].ToString();

        //=============Login ===============================
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                cEmpLogin emp = cEmpLogin.Get_ID(Convert.ToInt32(HttpContext.User.Identity.Name));
                if (emp != null)
                    return RedirectToAction("Index", "DashBoard");
                else
                    return View();


            }
            return View();
        }
        [HttpPost]
        public JsonResult Index(Login objlogin)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {

                if (objlogin.ForgotEmailID != null)
                {
                    string link = url + "/Login/ChangePassword?Email=" + EncryptDecrypt.Encrypt(objlogin.ForgotEmailID);
                    string Links = "<b><a target='_blank' href=" + link + ">Click here to reset your password.</a></b>";
                    string strBody = "";
                    StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/CreateAccount/ForgotPassword.html"));
                    strBody = sr.ReadToEnd();
                    sr.Close();
                    strBody = strBody.Replace("#Link#", Links);
                    strBody = strBody.Replace("#Employee#", objlogin.ForgotEmailID);
                    //strBody = strBody.Replace("#password#", password);
                    string str = Mail.SendEmail(objlogin.ForgotEmailID, "Reset Password", strBody, true);
                    return Json("4");
                }
                else
                {
                    List<cEmpLogin> objEmplog = cEmpLogin.Find(" sEmailID = " + objlogin.EmailID.Trim() + " and sPassword = " + objlogin.Password);

                    if (objEmplog.Count > 0)
                    {

                        if (objEmplog[0].sFirstTime == "0")
                        {
                            return Json("0");//Redirect to Change Password:
                        }
                        FormsAuthentication.SetAuthCookie(objEmplog[0].iID.ToString(), objlogin.RememberMe);

                        if (objEmplog[0].sFirstTime == "1")
                        {
                            return Json("1");//Redirect to My Profile:
                        }
                        return Json("2");//Redirect to DashBoard:
                    }
                    else
                    {
                        return Json("3");
                    }
                }
            }

            else
            {
                cEmpLogin emp = cEmpLogin.Get_ID(Convert.ToInt32(HttpContext.User.Identity.Name));
                return Json("2");
            }




        }

        ////====================== End Login ===================

        //// if(HttpContext.Current.User.Identity.IsAuthenticated)
        //public ActionResult CreateEmployee()
        //{
        //    int empID = 0;
        //    //===================================== Department Binding entity ============================================= //
        //    List<tblDepartment> deptList = (from data in db.tblDepartments
        //                                    orderby data.varDepartmentName ascending
        //                                    select data).ToList();
        //    tblDepartment objDept = new tblDepartment();
        //    objDept.varDepartmentName = "-- Select --";
        //    objDept.intDepartmentID = 0;
        //    deptList.Insert(0, objDept);
        //    SelectList objModelDept = new SelectList(deptList, "intDepartmentID", "varDepartmentName", 0);
        //    LoginViewModel objLogin = new LoginViewModel();
        //    objLogin.DepartmentModel = objModelDept;
        //    //===================================== End Department Binding ==================================================//


        //    //==================================== Start Designation Binding entity =======================================//
        //    List<tblDesignation> desigList = (from data in db.tblDesignations
        //                                      orderby data.varDesignationName ascending
        //                                      select data).ToList();
        //    tblDesignation objDesignation = new tblDesignation();
        //    objDesignation.varDesignationName = "-- Select --";
        //    objDesignation.intDesignationID = 0;
        //    desigList.Insert(0, objDesignation);
        //    SelectList objModelDesignation = new SelectList(desigList, "intDesignationID", "varDesignationName", 0);
        //    objLogin.DesignationModel = objModelDesignation;
        //    //=================================== End Designation Binding ================================================//


        //    //=================================== Employee Binding entity ================================================//
        //    List<tblEmployee> empList = (from data in db.tblEmployees
        //                                 orderby data.varFirstName ascending
        //                                 select data).ToList();
        //    var empIndex = empList.Find(item => item.varFirstName == "Azam");

        //    if (empIndex != null)
        //    {
        //        empID = empIndex.intEmployeeID;
        //    }
        //    else
        //    {
        //        empID = 0;
        //    }

        //    tblEmployee objemp = new tblEmployee();
        //    objemp.varFirstName = "-- Select --";
        //    objemp.intEmployeeID = 0;
        //    empList.Insert(0, objemp);
        //    SelectList objEmployeeModel = new SelectList(empList, "intEmployeeID", "varFirstName", empID);
        //    objLogin.EmployeeModel = objEmployeeModel;
        //    //=================================== End Employee Binding =====================================================//

        //    //======================Access Binding entity ==============================================================//
        //    //List<tblAccess> accessList = (from data in db.tblAccesses
        //    //                              orderby data.varAccessName ascending
        //    //                              select data).ToList();
        //    //SelectList objAccessList = new SelectList(accessList, "intAccessID", "varAccessName", 0);
        //    //objLogin.AccessList = objAccessList;
        //    //=====================End Access Binding =======================================================================//

        //    objLogin.access = new List<tblAccess>();
        //    objLogin.access = BindAccess();

        //    return View(objLogin);
        //}

        //[HttpPost]
        //public ActionResult CreateEmployee(LoginViewModel objtlogin, int[] AccessId)
        //{
        //    string accesslevels = "";
        //    try
        //    {

        //        //string accesslevels = string.Join(",", AccessId);
        //        //int lbc = objtlogin.access.Count;
        //        int count = 0;
        //        foreach (var item in objtlogin.access)
        //        {
        //            if (item.IsActive == true)
        //            {
        //                int accid = item.intAccessID;

        //                // string str=string.Join(",", accid);
        //                accesslevels = accesslevels + ',' + accid;
        //                count++;
        //            }
        //        }
        //        if (count > 0)
        //            accesslevels = accesslevels.Remove(0, 1);
        //        else
        //            accesslevels = "0";
        //        string strOfficialEmailId = objtlogin.objtblLogin.tblEmployee.varOfficeEmailAdd;
        //        string strUserName = objtlogin.objtblLogin.varLoginName;
        //        string strPassword = objtlogin.objtblLogin.varPassword;

        //        objtlogin.objtblLogin.tblEmployee.varAccessLevel = accesslevels;
        //        objtlogin.objtblLogin.varLoginName = strOfficialEmailId;
        //        objtlogin.objtblLogin.IsActive = true;

        //        db.tblLogins.Add(objtlogin.objtblLogin);
        //        db.SaveChanges();

        //        sendMail(strOfficialEmailId, strPassword);
        //        ViewBag.Message = "Data has been submitted successfully!!.";

        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
        //                    validationErrors.Entry.Entity.GetType().FullName,
        //                    validationError.PropertyName,
        //                    validationError.ErrorMessage);
        //            }
        //        }

        //        throw;  // You can also choose to handle the exception here...
        //    }
        //    return RedirectToAction("CreateEmployee");
        //}

        //public ActionResult ChangePassword(string Email)
        //{
        //    if (Email == null)
        //    {
        //        Session["Email"] = null;
        //    }
        //    else {
        //        Session["Email"] =EncryptDecrypt.Decrypt(Email).ToString();
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult ChangePassword(LoginViewModel lgvm)
        //{
        //    tblLogin tbllog = null;

        //    if (Session["Email"] != null)
        //    {
        //        string email = Session["Email"].ToString();
        //        using (var context = new HRIMSdbEntities())
        //        {
        //            context.Configuration.ProxyCreationEnabled = false;
        //            tbllog = context.tblLogins.Where(s => s.varLoginName ==email ).Single();
        //        }
        //        using (var context = new HRIMSdbEntities())
        //        {

        //            tbllog.varPassword = lgvm.varPassword;
        //            try
        //            {
        //                context.Entry(tbllog).State = EntityState.Modified;
        //                context.SaveChanges();

        //            }
        //            catch (DbUpdateConcurrencyException ex)
        //            {
        //                Console.WriteLine("Optimistic Concurrency exception occured");
        //            }
        //            Session["Email"] = null;
        //            return Json("1");
        //        }
        //    }
        //    else
        //    {
        //        if (HttpContext.User.Identity.IsAuthenticated)
        //        {
        //            using (var context = new HRIMSdbEntities())
        //            {
        //                context.Configuration.ProxyCreationEnabled = false;
        //                tbllog = context.tblLogins.Where(s => s.varLoginName == HttpContext.User.Identity.Name).Single();
        //            }
        //            using (var context = new HRIMSdbEntities())
        //            {

        //                tbllog.varPassword = lgvm.varPassword;
        //                try
        //                {
        //                    context.Entry(tbllog).State = EntityState.Modified;
        //                    context.SaveChanges();

        //                }
        //                catch (DbUpdateConcurrencyException ex)
        //                {
        //                    Console.WriteLine("Optimistic Concurrency exception occured");
        //                }
        //                return Json("2");
        //            }
        //        }
        //        else
        //        {
        //            return Json("3");
        //        }

        //    }


        //}

        //[HttpGet]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult ForgotPassword(ForgotPassword frgpass)
        //{
        //    string link = url + "/Login/ChangePassword?Email=" + EncryptDecrypt.Encrypt(frgpass.EmailID);
        //    string Links = "<b><a target='_blank' href=" + link + ">Click here to reset your password.</a></b>";
        //    string strBody = "";
        //    StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "App_Data/Forgotpassword.html"));
        //    strBody = sr.ReadToEnd();
        //    sr.Close();
        //    strBody = strBody.Replace("#Link#", Links);
        //    strBody = strBody.Replace("#Employee#", "Test");
        //    //strBody = strBody.Replace("#password#", password);
        //    string str = Mail.SendEmail(frgpass.EmailID, "Recover Password", strBody, true);
        //    return Json("1");
        //}

        //public ActionResult Email()
        //{
        //    return View();
        //}

        //public void sendMail(string email, string password)
        //{
        //    try
        //    {
        //        string strBody = "";
        //        StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "HTML/EmpCredential.html"));
        //        strBody = sr.ReadToEnd();
        //        sr.Close();
        //        strBody = strBody.Replace("#emailId#", email);
        //        strBody = strBody.Replace("#password#", password);
        //        string str = Mail.SendEmail(email, "Employee Credentials", strBody, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<tblAccess> BindAccess()
        //{
        //    List<tblAccess> objaccess = (from data in db.tblAccesses
        //                                 orderby data.varAccessName ascending
        //                                 select data).ToList();

        //    return objaccess;
        //}

        //[HttpPost]
        //public JsonResult CheckForDuplication(string EmailId)
        //{
        //    var data = db.tblEmployees.Where(p => p.varOfficeEmailAdd.Equals(EmailId, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //    // string ck = data.varOfficeEmailAdd;
        //    if (data != null)
        //    {
        //        return Json("1");

        //    }
        //    else
        //    {
        //        return Json("2");
        //    }


        //    return Json("KO");
        //}

        [HttpGet]
        public ActionResult ChangePassword(string Email)
        {
            ChangePassword omb = new ChangePassword();
            if (Email != null)
            {

                omb.hdnEmail = Email;
            }

            return View(omb);
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePassword objChangePass)
        {
            if (objChangePass.hdnEmail != null)
            {
                string email = EncryptDecrypt.Decrypt(objChangePass.hdnEmail);
                List<cEmpLogin> aobjLoginEmp = cEmpLogin.Find(" sEmailID = " + email);
                if (aobjLoginEmp.Count > 0)
                {
                    aobjLoginEmp[0].sPassword = objChangePass.NewPassword;
                    aobjLoginEmp[0].Save();
                    return Json("1");
                }
                else
                {

                    return Json("3");
                }

            }
            else
            {
                if (objChangePass.CurrentPassword != null && objChangePass.CurrentPassword != "")
                {
                    int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    cEmpLogin objLoginEmp = cEmpLogin.Get_ID(loginID);
                    objLoginEmp.sPassword = objChangePass.NewPassword;
                    objLoginEmp.Save();
                    return Json("2");
                }
                else
                {

                    return Json("3");
                }
            }

        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        //[HttpGet]
        public JsonResult CheckEmailValid(string ForgotEmailID)
        {
            try
            {

                List<cEmpLogin> objEmplog = cEmpLogin.Find(" sEmailID = " + ForgotEmailID.Trim());
                if (objEmplog.Count == 0)
                {
                    return Json("This email id is not registered with us.", JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public JsonResult CheckOldPassword(string CurrentPassword)
        {
            try
            {
                int loginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                cEmpLogin objLoginEmp = cEmpLogin.Get_ID(loginID);

                if (objLoginEmp.sPassword == CurrentPassword.Trim())
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Incorrect old password", JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception)
            {

                throw;
            }

        }



        //public ActionResult Error() {
        //    return View();
        //}

    }
}
