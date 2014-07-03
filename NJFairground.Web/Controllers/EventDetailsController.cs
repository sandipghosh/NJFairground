

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class EventDetailsController : Controller
    {
        private readonly IEventDataRepository _eventDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailsController"/> class.
        /// </summary>
        /// <param name="eventDataRepository">The event data repository.</param>
        public EventDetailsController(IEventDataRepository eventDataRepository)
        {
            this._eventDataRepository = eventDataRepository;
        }

        /// <summary>
        /// Indexes the specified page identifier.
        /// </summary>
        /// <param name="PageId">The page identifier.</param>
        /// <param name="EventId">The event identifier.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index(int PageId, int EventId)
        {
            EventModel eventItem = this._eventDataRepository.GetList(x => x.PageId == PageId && x.EventId == EventId).FirstOrDefault();
            return View("Index.mobile", eventItem);
        }
    }
}
