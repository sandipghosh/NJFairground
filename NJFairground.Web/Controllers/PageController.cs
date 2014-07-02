

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Utilities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class PageController : BaseController
    {
        private readonly IPageDataRepository _pageDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageController"/> class.
        /// </summary>
        /// <param name="pageDataRepository">The page data repository.</param>
        public PageController(IPageDataRepository pageDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            try
            {
                var pages = this._pageDataRepository.GetList(x => x.StatusId == 1, x => x.PageId, false).ToList();
                return View("Index.mobile",pages);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return null;
        }

    }
}
