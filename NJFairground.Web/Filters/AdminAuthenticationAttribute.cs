using NJFairground.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace NJFairground.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AdminAuthenticationAttribute
        : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.Controller.ControllerContext.HttpContext.Session["UserId"] == null
                && filterContext.Controller.ControllerContext.HttpContext.Session["UserId"].AsString() != CommonUtility.GetAppSetting<string>("AdminUserId"))
            {
                string redirectionUrl = string.Empty;
                if (filterContext.RequestContext.HttpContext.Request.HttpMethod == System.Net.Http.HttpMethod.Get.ToString().ToUpper())
                {
                    redirectionUrl = filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri.ToBase64Encode();
                }

                RedirectToRouteResult redirect = new RedirectToRouteResult("Login", new RouteValueDictionary(new
                {
                    redirectionUrl = redirectionUrl
                }));

                filterContext.Result = redirect;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}