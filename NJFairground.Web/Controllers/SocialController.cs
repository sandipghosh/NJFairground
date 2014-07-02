
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class SocialController : BaseController
    { 
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialController"/> class.
        /// </summary>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public SocialController(IPageItemDataRepository pageItemDataRepository)
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
            List<string> NavItems = new List<string>();
            //NavItems.Add("The Fair");
            //NavItems.Add("Fun");
            //NavItems.Add("Info");
            //NavItems.Add("Social");
            //NavItems.Add("Map");

            ViewBag.NavBarItems = NavItems;
            List<PageItemModel> pageItems = this._pageItemDataRepository.GetList(x => x.PageId == Convert.ToInt32(Page.Social) && x.StatusId == 1).ToList();
            return View("Index.mobile", pageItems);
        }
    }
}
