using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquatroHRIMS.ActionFilters;
using AquatroHRIMS.Models;
using HRIMS;
using AquatroHRIMS.ViewModel;
using System.Data;
namespace AquatroHRIMS.Controllers
{
    //[HRIMSActionFilter]
    //[CustomException]
    public class PerformanceController : Controller
    {
        //
        // GET: /Performance/

        public ActionResult Index(string flag)
        {

            if (flag == null || flag=="0")
            {

                QuadrantMeasuresViewModel objQuadrantMeasure = new QuadrantMeasuresViewModel();
                try
                {
                    int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    List<lstSetQuadMeasures> listQuadMeasure = new List<lstSetQuadMeasures>();
                    DataTable dt = cQuadrantMeasure.getSetQuadrantMeasuresList(LoginID);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            listQuadMeasure.Add(new lstSetQuadMeasures { GoalID = Convert.ToInt32(dt.Rows[i]["varGoalID"].ToString()), Measures = dt.Rows[i]["varQuadrantMeasure"].ToString(), Count = i + 1, DeptID = dt.Rows[i]["varDepartmentType"].ToString(), dataCount = Convert.ToInt32(dt.Rows[i]["varcount"].ToString()), AllLevelFlag = dt.Rows[i]["varAllLevels"].ToString() });
                        }
                        objQuadrantMeasure.lstQudMeasuresList = listQuadMeasure;
                    }

                    objQuadrantMeasure.DepartmentTypeModel = getDepartmentTypeID();
                    objQuadrantMeasure.GoalTileModel = getGoalTitleList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View(objQuadrantMeasure);
            }
            else {
                try
                {
                    int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    List<cQuadrantMeasure> aob = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID + " and objDepartmentType = " + flag);
                    List<lstSetQuadMeasures> listQuadMeasure = new List<lstSetQuadMeasures>();
                    if (aob.Count > 0)
                    {
                        int count = 0;
                        foreach (var item in aob)
                        {
                            count++;

                            listQuadMeasure.Add(new lstSetQuadMeasures { GoalID = item.objQuadrant.iObjectID, Measures = item.sMeasures, Count = count, DeptID = item.objFunctionalGroup.iObjectID.ToString(), dataCount = 0, AllLevelFlag = item.bIsAllLevel.ToString() });
                        }
                        objQuadrantMeasure.lstQudMeasuresList = listQuadMeasure;
                    }
                    objQuadrantMeasure.DepartmentTypeModel = getDepartmentTypeID();
                    objQuadrantMeasure.GoalTileModel = getGoalTitleList();
                    ViewBag.Depart = flag;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View(objQuadrantMeasure);
            }

        }
        //Added Swpnesh:-

        //public ActionResult AddQuadrants()
        //{
        //    AddQuadrant quadrant = new AddQuadrant();
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return View(quadrant);
        //}

        public JsonResult AddQuadrants(string QuadrantName)
        {
            try
            {
                cQuadrant goal = cQuadrant.Create();
                goal.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                goal.sName = QuadrantName.ToString();
                goal.bIsActive = true;
                goal.Save();
                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        QuadrantMeasuresViewModel objQuadrantMeasure = new QuadrantMeasuresViewModel();
        private SelectList getDepartmentTypeID()
        {
            try
            {
                List<ddlDepartmentTypeList> objDeptTypeList = new List<ddlDepartmentTypeList>();
                List<cFunctionalGroup> objDeptType = cFunctionalGroup.Find();
                //objDeptTypeList.Add(new ddlDepartmentTypeList { Value = 0, Text = "--select--" });
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
                List<cQuadrant> objGoalTitle = cQuadrant.Find(" objEmpLogin = " + LoginID);

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
        [HttpPost]
        public JsonResult GetQuadrantList()
        {
            try
            {
                JsonResult result = new JsonResult();
                //int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                //List<ddlGoalTitleList> objGoalTitleList = new List<ddlGoalTitleList>();
                //List<cGoals> objGoalTitle = cGoals.Find(" objEmpLogin = " + LoginID);
                //foreach (var item in objGoalTitle)
                //{
                //    objGoalTitleList.Add(new ddlGoalTitleList { Value = item.iID, Text = item.sName });
                //}
                //SelectList objGoalList = new SelectList(objGoalTitleList, "Value", "Text");

                QuadrantMeasuresViewModel objQuadrantMeasure = new QuadrantMeasuresViewModel();
                objQuadrantMeasure.DepartmentTypeModel = getDepartmentTypeID();
                objQuadrantMeasure.GoalTileModel = getGoalTitleList();
                result.Data = objQuadrantMeasure;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;
                //return Json(objEmpViewMod);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult SaveData(List<string> Goal, List<string> Comment, List<string> all, List<string> Depaerment, string Total)
        {
            try
            {
                int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
                DeleteQuadrant(LoginID);

                for (int i = 0; i < Convert.ToInt32(Total); i++)
                {
                    if (Depaerment[i] == "")
                    {
                        Depaerment[i] = "0,";
                    }
                    string s = Depaerment[i].Substring(0, Depaerment[i].Length - 1);
                    string[] Dep = s.Split(',');
                    foreach (string item in Dep)
                    {
                        cQuadrantMeasure objQuadrant = cQuadrantMeasure.Create();
                        objQuadrant.sMeasures = Comment[i];
                       // objQuadrant.objEmpLogin.iObjectID = Convert.ToInt32(HttpContext.User.Identity.Name);
                        if (Goal[i] != null)
                            objQuadrant.objQuadrant.iObjectID = Convert.ToInt32(Goal[i]);
                        else
                            objQuadrant.objQuadrant.iObjectID = Convert.ToInt32(0);
                       // objQuadrant.bIsActive = true;
                        if (all[i] == "0")
                            objQuadrant.bIsAllLevel = false;
                        else
                            objQuadrant.bIsAllLevel = true;

                        objQuadrant.objFunctionalGroup.iObjectID = Convert.ToInt32(item);
                        objQuadrant.Save();
                    }


                }

                return Json("1");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult QuadMeasuresList(int id)
        {
            try
            {
            //    int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
            //    List<cQuadrantMeasure> aob = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID + " and objDepartmentType = " + id);
            //    List<lstSetQuadMeasures> listQuadMeasure = new List<lstSetQuadMeasures>();
            //    if (aob.Count > 0)
            //    {
            //        int count = 0;
            //        foreach (var item in aob)
            //        {
            //            count++;

            //            listQuadMeasure.Add(new lstSetQuadMeasures { GoalID = item.objGoals.iObjectID, Measures = item.sMeasures, Count = count, DeptID = item.objDepartmentType.iObjectID.ToString(), dataCount = 0, AllLevelFlag = item.bAllLevel.ToString() });
            //        }
            //        objQuadrantMeasure.lstQudMeasuresList = listQuadMeasure;
            //    }
            //    objQuadrantMeasure.DepartmentTypeModel = getDepartmentTypeID();
            //    objQuadrantMeasure.GoalTileModel = getGoalTitleList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return View(objQuadrantMeasure);
            return RedirectToAction("Index", "Performance", new { flag = id });
        }

        public JsonResult ReleaseQuadrants() {
            //try
            //{
            //    int LoginID = Convert.ToInt32(HttpContext.User.Identity.Name);
            //    List<cQuadrantMeasure> aobjRel = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID);
            //    if (aobjRel.Count > 0)
            //    {
            //        for (int i = 0; i < aobjRel.Count; i++)
            //        {
            //            aobjRel[i].bReleaseQuadrant = true;
            //            aobjRel[i].Save();
            //        }

            //        return Json("1");
                    
            //    }
            //    else
            //    {
            //        return Json("2");
            //    }
             
            //}
            //catch (Exception ex)
            //{
                
            //    throw ex;
            //}
            return Json("1");
        
        }

        //Delete Data Quadrant:-
        public void DeleteQuadrant(int LoginID)
        {
            List<cQuadrantMeasure> aobQuadrant = cQuadrantMeasure.Find(" objEmpLogin = " + LoginID);
            if (aobQuadrant.Count > 0)
            {
                foreach (var item in aobQuadrant)
                {
                    cQuadrantMeasure.Delete(item.iID);
                }
            }
        }
    }
}