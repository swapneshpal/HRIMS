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
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{
            //    cEmpLogin emp = cEmpLogin.Get_ID(Convert.ToInt32(HttpContext.User.Identity.Name));
            //    if (emp != null)
            //    {
            //        //List<cEmpPersonalDetails> aobEmp = cEmpPersonalDetails.Find(" objEmpLogin = " + emp.iID);
            //        //if (aobEmp.Count > 0)
            //        //{
            //        //    Session["LoginName"] = aobEmp[0].sFirstName + " " + aobEmp[0].sLastName;
            //        //}
            //        return RedirectToAction("Index", "DashBoard");
            //    }
            //    else
            //    {
            //          return View();
            //    }
            //}
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
                    StreamReader sr = new StreamReader(HttpContext.Server.MapPath(HttpContext.Request.ApplicationPath + "/App_Data/ForgotPassword.html"));
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
                        string UserName = "";
                        List<cEmpPersonalDetails> aobEmp = cEmpPersonalDetails.Find(" objEmpLogin = " + objEmplog[0].iID);
                        if (aobEmp.Count > 0)
                        {
                            UserName = aobEmp[0].sFirstName + " " + aobEmp[0].sLastName;
                        }
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
            Session["LoginName"] = null;
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

        
      
    }
}
