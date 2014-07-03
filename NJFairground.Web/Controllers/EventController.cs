

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class EventController : Controller
    {
        private readonly IEventDataRepository _eventDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        /// <param name="eventDataRepository">The event data repository.</param>
        public EventController(IEventDataRepository eventDataRepository)
        {
            this._eventDataRepository = eventDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            List<EventModel> eventItems = this._eventDataRepository.GetList(x => x.PageId == Convert.ToInt32(NJFairground.Web.Models.Page.Event) && x.StatusId == 1).ToList();
            return View("Index.mobile", eventItems);
        }

    }
}
