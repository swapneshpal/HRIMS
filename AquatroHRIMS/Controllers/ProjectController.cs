using HRIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.ViewModel;
using AquatroHRIMS.Models;
using AquatroHRIMS.ActionFilters;

namespace AquatroHRIMS.Controllers
{
    [HRIMSActionFilter]
    [CustomException]
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        public ActionResult Index()
        {
            return View();
        }


        private SelectList getInternalHeadList()
        {
            try
            {
                List<ddlInternalHead> objInternalHead = new List<ddlInternalHead>();

                List<cEmpPersonalDetails> objEmployee = cEmpPersonalDetails.Find();

                foreach (var itememp in objEmployee)
                {
                    objInternalHead.Add(new ddlInternalHead { Value = itememp.iID, Text = itememp.sFirstName });
                }
                SelectList objinternalheadlist = new SelectList(objInternalHead, "Value", "Text");
                return objinternalheadlist;
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
                List<ddlClient> objClientList = new List<ddlClient>();

                List<cClient> objClient = cClient.Find();

                foreach (var item in objClient)
                {
                    objClientList.Add(new ddlClient { Value = item.iID, Text = item.sName });
                }
                SelectList objclientlistvalue = new SelectList(objClientList, "Value", "Text");
                return objclientlistvalue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getExternalHeadList()
        {
            try
            {
                List<ddlExternalHead> objExternalHeadList = new List<ddlExternalHead>();

                List<cExternalProjectHead> objExternalHead = cExternalProjectHead.Find();

                foreach (var item in objExternalHead)
                {
                    objExternalHeadList.Add(new ddlExternalHead { Value = item.iID, Text = item.sName });
                }
                SelectList objclientlistvalue = new SelectList(objExternalHeadList, "Value", "Text");
                return objclientlistvalue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private SelectList getStatusList()
        {
            try
            {
                List<ddlstatus> objstusList = new List<ddlstatus>();

                List<cStatus> objStatus = cStatus.Find();

                foreach (var item in objStatus)
                {
                    objstusList.Add(new ddlstatus { Value = item.iID, Text = item.sName });
                }
                SelectList objStatusValue = new SelectList(objstusList, "Value", "Text");
                return objStatusValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AddProject(string ID)
        {
            ProjectViewModel objEmpViewMod = new ProjectViewModel();
            if (ID != null)
            {
                Project aProject = new Project();
                cProject aobPro = cProject.Get_ID(Convert.ToInt32(ID));
                objEmpViewMod.hdnProjectID = aobPro.iID.ToString();
                aProject.ProjectName = aobPro.sProjectName;
                aProject.Description = aobPro.sDescription;
                aProject.StartDate = aobPro.dtStartDate;
                aProject.EndDate = aobPro.dtEndDate;
                ViewBag.status = aobPro.objStatus.iObjectID;
                ViewBag.InternalHead = aobPro.iInternalHead;
                ViewBag.ExternalHead = aobPro.objExternalProjectHead.iObjectID;
                ViewBag.ClientID = aobPro.objClient.iObjectID;
                objEmpViewMod.objProject = aProject;
            }
            try
            {

                objEmpViewMod.ClientList = getIClientList();
                objEmpViewMod.ExternalHeadList = getExternalHeadList();
                objEmpViewMod.Internalheadlist = getInternalHeadList();
                objEmpViewMod.StatusList = getStatusList();
                return View(objEmpViewMod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult AddProject(ProjectViewModel objprojectviewmodel)
        {
            try
            {
                if (objprojectviewmodel.SelectedClient[0] == "" || objprojectviewmodel.SelectedInternalHead[0] == "" || objprojectviewmodel.SelectedExternalHead[0] == "")
                {

                    return Json("2");
                }


                else
                {
                    if (objprojectviewmodel.hdnProjectID != null && objprojectviewmodel.hdnProjectID != "")
                    {
                        cProject objproject = cProject.Get_ID(Convert.ToInt32(objprojectviewmodel.hdnProjectID));
                        objproject.sProjectName = objprojectviewmodel.objProject.ProjectName;

                        string ClientId = objprojectviewmodel.SelectedClient[0].ToString();
                        objproject.objClient.iObjectID = Convert.ToInt32(ClientId);

                        string interHeadID = objprojectviewmodel.SelectedInternalHead[0].ToString();
                        objproject.iInternalHead = Convert.ToInt32(interHeadID);

                        string iStatus = objprojectviewmodel.SelectedStatus[0].ToString();
                        objproject.objStatus.iObjectID = Convert.ToInt32(iStatus);

                        string iExternalHeadID = objprojectviewmodel.SelectedExternalHead[0].ToString();
                        objproject.objExternalProjectHead.iObjectID = Convert.ToInt32(iExternalHeadID);

                        objproject.sDescription = objprojectviewmodel.objProject.Description;

                        objproject.dtStartDate = objprojectviewmodel.objProject.StartDate;

                        objproject.dtEndDate = objprojectviewmodel.objProject.EndDate;
                        //objproject.sCompletion = objprojectviewmodel.Complition;
                        objproject.Save();
                        return Json("3");
                    }
                    else
                    {
                        cProject objproject = cProject.Create();
                        objproject.sProjectName = objprojectviewmodel.objProject.ProjectName;

                        string ClientId = objprojectviewmodel.SelectedClient[0].ToString();
                        objproject.objClient.iObjectID = Convert.ToInt32(ClientId);

                        string interHeadID = objprojectviewmodel.SelectedInternalHead[0].ToString();
                        objproject.iInternalHead = Convert.ToInt32(interHeadID);

                        string iStatus = objprojectviewmodel.SelectedStatus[0].ToString();
                        objproject.objStatus.iObjectID = Convert.ToInt32(iStatus);

                        string iExternalHeadID = objprojectviewmodel.SelectedExternalHead[0].ToString();
                        objproject.objExternalProjectHead.iObjectID = Convert.ToInt32(iExternalHeadID);
                        objproject.dtStartDate = objprojectviewmodel.objProject.StartDate;

                        objproject.dtEndDate = objprojectviewmodel.objProject.EndDate;
                        objproject.sDescription = objprojectviewmodel.objProject.Description;
                        //objproject.sCompletion = objprojectviewmodel.Complition;
                        objproject.Save();
                        return Json("1");
                    }

                   
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        public ActionResult AssignProject()
        {
            try
            {
                ProjectViewModel objProjectView = new ProjectViewModel();
                objProjectView.ProjectList = getProjectList();
                objProjectView.EmpList = getEmployeeList();
                return View(objProjectView);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult AssignProject(ProjectViewModel objprojectviewmodel)
        {
            try
            {
                if (objprojectviewmodel.SelectedEmpList.Count() > 0)
                {
                    foreach (var item in objprojectviewmodel.SelectedEmpList)
                    {
                        cProjectAssigned objProjectAssign = cProjectAssigned.Create();
                        objProjectAssign.objProject.iObjectID = Convert.ToInt32(objprojectviewmodel.SelectedProject[0]);
                        objProjectAssign.objEmpLogin.iObjectID = Convert.ToInt32(item.ToString());
                        objProjectAssign.sDescription = objprojectviewmodel.ProjectDescription;
                        objProjectAssign.bIsActive = true;
                        objProjectAssign.Save();
                    }


                }
                return Json("1");

                //ProjectViewModel objProjectView = new ProjectViewModel();
                //objProjectView.ProjectList = getProjectList();
                //objProjectView.EmpList = getEmployeeList();
                //return View(objProjectView);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult MyProject()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SelectList getProjectList()
        {

            try
            {
                List<ddlProject> objProject = new List<ddlProject>();
                List<cProject> aobjProject = cProject.Find();
                foreach (var itemProject in aobjProject)
                {
                    objProject.Add(new ddlProject { Value = itemProject.iID, Text = itemProject.sProjectName });
                }

                SelectList objProjectList = new SelectList(objProject, "Value", "Text");
                return objProjectList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private SelectList getEmployeeList()
        {
            try
            {
                List<ddlEmpList> objEmpdList = new List<ddlEmpList>();
                List<cEmpPersonalDetails> objEmp = cEmpPersonalDetails.Find();

                foreach (var item in objEmp)
                {
                    objEmpdList.Add(new ddlEmpList { Value = item.objEmpLogin.iObjectID, Text = item.sFirstName });
                }
                SelectList objEmployeeList = new SelectList(objEmpdList, "Value", "Text");
                return objEmployeeList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public ActionResult ProjectList()
        {
            try
            {
                List<cProject> aobjProject = cProject.Find();

                List<ProjectList> objProject = new List<ProjectList>();

                foreach (var item in aobjProject)
                {
                    string InternalHeadName = "";
                    List<cEmpPersonalDetails> aobPers = cEmpPersonalDetails.Find(" objEmpLogin = " + item.iInternalHead);
                    if (aobPers.Count > 0)
                    {
                        InternalHeadName = aobPers[0].sFirstName + " " + aobPers[0].sLastName;
                    }
                    string ExternalHeadname = cExternalProjectHead.Get_ID(Convert.ToInt32(item.objExternalProjectHead.iObjectID)).sName.ToString();
                    objProject.Add(new ProjectList { ProjectID = item.iID, ProjectName = item.sProjectName, InternalHead = InternalHeadName, ExternalHead = ExternalHeadname, Status = cStatus.Get_ID(item.objStatus.iObjectID).sName.ToString() });
                }
                return View(objProject);
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
        //        int val = Convert.ToInt32(ID);
        //        Client objClientModel = new Client();
        //        cClient objClient = cClient.Get_ID(val);

        //        objClientModel.ClientId = objClient.iID;
        //        objClientModel.ClientName = objClient.sName;
        //        objClientModel.Contact = objClient.sContactNo;
        //        objClientModel.Address = objClient.sAddress;
        //        objClientModel.Description = objClient.sDescription;
        //        objClientModel.EmailID = objClient.sEmailID;
        //        result.Data = objClientModel;
        //        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public JsonResult Delete(string ID)
        //{
        //    try
        //    {
        //        List<cProjectAssigned> aobProjectAssiged = cProjectAssigned.Find();
        //        int id = Convert.ToInt32(ID);
        //        cProject.Delete(id);

        //        return Json("1");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
    }
}