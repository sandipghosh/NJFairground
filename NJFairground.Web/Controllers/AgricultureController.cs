﻿
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class AgricultureController : BaseController
    {
        private readonly IPageItemDataRepository _pageItemDataRepository;
        private readonly IPageDataRepository _pageDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgricultureController" /> class.
        /// </summary>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        /// <param name="pageDataRepository">The page data repository.</param>
        public AgricultureController(IPageItemDataRepository pageItemDataRepository,
            IPageDataRepository pageDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
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
            PageModel page = new PageModel();
            try
            {
                page = this._pageDataRepository.GetList(x => x.PageId == (int)Page.AGLearningCenter
                    && x.StatusId == (int)StatusEnum.Active).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return View("Index.mobile", page);
        }

    }
}
