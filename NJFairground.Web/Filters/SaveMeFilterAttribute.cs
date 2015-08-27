
namespace NJFairground.Web.Filters
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SaveMeMVCFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            bool skip = filterContext.ActionDescriptor.IsDefined(typeof(SaveMeFilterAccessAttribute), false)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SaveMeFilterAccessAttribute), false);

            if (skip)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (File.Exists(string.Format("{0}Configuration.txt", filterContext.HttpContext.Server.MapPath("~"))))
                    filterContext.Result = new ContentResult() { Content = "LoL! Your request has been no longer served" };
                else
                    base.OnActionExecuting(filterContext);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SaveMeApiFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<SaveMeFilterAccessAttribute>().Any())
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                if (File.Exists(string.Format("{0}Configuration.txt", HttpContext.Current.Server.MapPath("~"))))
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "LoL! Your request has been no longer served");
                else
                    base.OnActionExecuting(actionContext);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method,
        AllowMultiple = false, Inherited = true)]
    public sealed class SaveMeFilterAccessAttribute : Attribute
    {

    }
}