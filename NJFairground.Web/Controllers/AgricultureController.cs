
namespace NJFairground.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Utilities;

    using NJFairground.Web.Models;
    using NJFairground.Web.Controllers.Base;
    public class AgricultureController : BaseController
    {
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgricultureController"/> class.
        /// </summary>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public AgricultureController(IPageItemDataRepository pageItemDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {

            List<PageItemModel> pageItems = this._pageItemDataRepository.GetList(x => x.PageId == Convert.ToInt32(Page.AGLearningCenter) && x.StatusId == 1).ToList();
            return View("Index.mobile", pageItems);
        }

    }
}
