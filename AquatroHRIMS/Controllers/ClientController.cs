using AquatroHRIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRIMS;
using AquatroHRIMS.ActionFilters;
namespace AquatroHRIMS.Controllers
{
    [HRIMSActionFilter]
    [CustomException]
    public class ClientController : Controller
    {
        //
        // GET: /Client/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddClient(string ID)
        {
            try
            {
                Client objModelClient = new Client();
                if(ID!=null)
                {
                    cClient objClient = cClient.Get_ID(Convert.ToInt32(ID));
                    objModelClient.ClientIDHdn = objClient.iID.ToString();
                    objModelClient.ClientName = objClient.sName;
                    objModelClient.EmailIDUpdate = objClient.sEmailID;
                    objModelClient.ContactUpdate = objClient.sContactNo;
                    objModelClient.Address = objClient.sAddress;
                    objModelClient.Description = objClient.sDescription;
                }
                return View(objModelClient);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult ClientList()
        {
            try
            {
                List<cClient> aobjClient = cClient.Find();

                List<ClientList> objClient = new List<ClientList>();

                foreach (var item in aobjClient)
                {
                    objClient.Add(new ClientList { ClientID = item.iID, ClientName = item.sName, ClientEmail = item.sEmailID, ClientContact = item.sContactNo });
                }
                return View(objClient);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        public JsonResult AddClient(Client objAddClient, string ClientIDHdn)
        {
            try
            {
                if (ClientIDHdn != null && ClientIDHdn != "")
                {
                    int id = Convert.ToInt32(ClientIDHdn);
                    cClient objClient = cClient.Get_ID(id);
                    objClient.sName = objAddClient.ClientName;
                    objClient.sEmailID   = objAddClient.EmailIDUpdate;
                    objClient.sContactNo = objAddClient.ContactUpdate;
                    objClient.sAddress = objAddClient.Address;
                    objClient.sDescription = objAddClient.Description;
                    objClient.Save();
                    return Json("2");
                }
                else {
                    cClient objClient = cClient.Create();
                    objClient.sName = objAddClient.ClientName;
                    objClient.sEmailID = objAddClient.EmailID;
                    objClient.sContactNo = objAddClient.Contact;
                    objClient.sAddress = objAddClient.Address;
                    objClient.sDescription = objAddClient.Description;
                    objClient.Save();
                    return Json("3");
                }
              
            
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }

        public ActionResult AddExternalHead() {
            Externalhead ExtHead = new Externalhead();
            ExtHead.ExternalClientList = getIClientList();
            return View(ExtHead);
        }

        [HttpPost]
        public ActionResult  AddExternalHead(Externalhead head)
        {
            try
            {
                cExternalProjectHead ExtHead = cExternalProjectHead.Create();
                ExtHead.sName = head.Name;
                ExtHead.sEmailID = head.Email;
                ExtHead.sContactNo = head.Contact;
                ExtHead.objClient.iObjectID = Convert.ToInt32(head.SelectedExternalClient[0]);
                ExtHead.bIsActive = true;
                ExtHead.Save();
                head.ExternalClientList = getIClientList();
                ViewBag.DataSaved = "External head added successfully";
                return View(head);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private SelectList getIClientList()
        {
            try
            {
                List<ddlExternalClient> objClientList = new List<ddlExternalClient>();

                List<cClient> objClient = cClient.Find();
                objClientList.Add(new ddlExternalClient { Value = 0, Text = "--select--" });
                foreach (var item in objClient)
                {
                    objClientList.Add(new ddlExternalClient { Value = item.iID, Text = item.sName });
                }
                SelectList objclientlistvalue = new SelectList(objClientList, "Value", "Text");
                return objclientlistvalue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult Delete(string ID)
        {
            try
            {
                int id = Convert.ToInt32(ID);
                cClient.Delete(id);

                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public JsonResult CheckClientEmail(string EmailID)
        {
            try
            {
                List<cClient> objLogin = cClient.Find(" sEmailID = " + EmailID.ToString().Trim());
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

        public JsonResult CheckClientEmailUpdate(string EmailIDUpdate, string ClientIDHdn)
        {
            try
            {
                int ID = Convert.ToInt32(ClientIDHdn);
           
                List<cClient> objLogin = cClient.Find(" sEmailID = " + EmailIDUpdate.ToString().Trim());


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

        public JsonResult CheckClientContact(string Contact)
        {
            try
            {
                List<cClient> objLogin = cClient.Find(" sContactNo = " + Contact.ToString().Trim());
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

        public JsonResult CheckClientContactUpdate(string ContactUpdate, string ClientIDHdn)
        {
            try
            {
                int ID = Convert.ToInt32(ClientIDHdn);
                List<cClient> objLogin = cClient.Find(" sContactNo = " + ContactUpdate.ToString().Trim());
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
    }



}