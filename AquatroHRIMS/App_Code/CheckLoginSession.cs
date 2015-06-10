using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquatroHRIMS.App_Code
{
    public class CheckLoginSession:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var session = filterContext.HttpContext.Session;

            if (!(HttpContext.Current.User.Identity.IsAuthenticated))
            {
                if (request.IsAjaxRequest())
                {
                    response.StatusCode = 590;

                }
                else
                {
                    var url = new UrlHelper(filterContext.HttpContext.Request.RequestContext);
                    if (!response.IsRequestBeingRedirected)
                        response.Redirect(url.Action("Index", "Login"));
                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}