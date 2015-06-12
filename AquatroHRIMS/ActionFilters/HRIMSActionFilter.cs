using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HRIMS;
using AquatroHRIMS.App_Code;

namespace AquatroHRIMS.ActionFilters
{
    public class HRIMSActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(HttpContext.Current.User.Identity.IsAuthenticated))
            {

                var url = new UrlHelper(filterContext.HttpContext.Request.RequestContext);
                filterContext.HttpContext.Response.Redirect(url.Action("Index", "Login"));

              
            }
            else
            {
                int i = Log("OnActionExecuting", filterContext.RouteData);
                if (i == 0)
                {
                    var context = new RequestContext(new HttpContextWrapper(System.Web.HttpContext.Current), new RouteData());
                    var urlHelper = new UrlHelper(context);
                    var url = urlHelper.Action("Denied", "PageAccess");
                    System.Web.HttpContext.Current.Response.Redirect(url);
                }
                base.OnActionExecuting(filterContext);
            }

        }

        private int Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            string str = controllerName + "/" + actionName;
            int id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            cEmpLogin objlogon = cEmpLogin.Get_ID(id);
            int access = objlogon.objRoleAccess.iObjectID;
            if(access==1 || access==2)
            {
                return 1;
            }
            List<cPermission> objPermit = cPermission.Find(" sName = " + str);
            if (objPermit.Count > 0)
            {
                List<cPermissionRoleAccess> objPermission = cPermissionRoleAccess.Find(" objRoleAccess = " + objlogon.objRoleAccess.iObjectID + " and objPermission = " + objPermit[0].iID);
                if (objPermission.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }

            }
            else
            {
                return 0;
            }
        }

    }
    public class CustomException : HandleErrorAttribute
    {
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                var context = new RequestContext(new HttpContextWrapper(System.Web.HttpContext.Current), new RouteData());
                var urlHelper = new UrlHelper(context);
                filterContext.Result = new RedirectResult("../PageAccess/Error"); 
                //var url = urlHelper.Action("Error", "PageAccess");
                //System.Web.HttpContext.Current.Response.Redirect(url);
            }

            // if the request is AJAX return JSON else view.
            if (IsAjax(filterContext))
            {
                //Because its a exception raised after ajax invocation
                //Lets return Json
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.Result = new RedirectResult("../PageAccess/Error"); 
                HttpContext.Current.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                //Normal Exception
                //  base.OnException(filterContext);
                // Http FormsAuthentication.SignOut();
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    var context = new RequestContext(new HttpContextWrapper(System.Web.HttpContext.Current), new RouteData());
                    var urlHelper = new UrlHelper(context);
                    var url = urlHelper.Action("Error", "PageAccess");
                    System.Web.HttpContext.Current.Response.Redirect(url);
                }
            }

            // Write error logging code here if you wish.

            //if want to get different of the request
            var currentController = (string)filterContext.RouteData.Values["controller"];
            var currentActionName = (string)filterContext.RouteData.Values["action"];
            string strBody = "Controller=" + currentController + "<br>Action:=" + currentActionName + "<br>Error=" + filterContext.Exception.Message;
            string msg = Mail.SendEmail("dharam3579@gmail.com", "Error Details", strBody, true);
            //Write Mail For Alert to Webmaster:-
        }
    }
}