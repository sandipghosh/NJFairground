

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Areas.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;

    public class EventSchedulerController : BaseController
    {
        private readonly IEventDataRepository _eventDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSchedulerController"/> class.
        /// </summary>
        /// <param name="eventDataRepository">The event data repository.</param>
        public EventSchedulerController(IEventDataRepository eventDataRepository)
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
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public JsonResult GetEventData()
        {
            try
            {
                IList<EventModel> data = this.GetEvents();
                return Json(new
                {
                    Status = ResponseStatus.success.ToString(),
                    Data = data.Select(x => new SchedularSchema
                    {
                        id = x.EventId,
                        pageid = x.PageId,
                        title = x.EventTitle,
                        start = x.StartDate,
                        end = x.EndDate,
                        statusid = x.StatusId,
                        description = x.EventDesc
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Json(new { Status = ResponseStatus.failure.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public PartialViewResult GetAddEventDialog()
        {
            SchedularSchema eventData = new SchedularSchema
            {
                statusid = (int)StatusEnum.Active,
                pageid = (int)Page.Event
            };
            return PartialView("_AddEvent", eventData);
        }

        [AcceptVerbs(HttpVerbs.Post),
        OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public JsonResult SetEventData(SchedularSchema schedularData)
        {
            try
            {
                this.SetEvent(schedularData);
                return Json(new
                {
                    Status = ResponseStatus.success.ToString(),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Json(new { Status = ResponseStatus.failure.ToString() }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns></returns>
        private IList<EventModel> GetEvents()
        {
            IList<EventModel> eventData = new List<EventModel>();
            try
            {
                eventData = this._eventDataRepository
                    .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                if (!eventData.IsEmptyCollection())
                {
                    return eventData;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return eventData;
        }

        private void SetEvent(SchedularSchema schedularData)
        {
            try
            {
                EventModel eventData = null;

                if (schedularData.id > 0)
                {
                    eventData = this._eventDataRepository.Get(schedularData.id);
                    eventData = Mapper.Map<SchedularSchema, EventModel>(schedularData);
                    eventData.CreatedOn = DateTime.Now;
                    this._eventDataRepository.Update(eventData);
                }
                else
                {
                    eventData = Mapper.Map<SchedularSchema, EventModel>(schedularData);
                    eventData.CreatedOn = DateTime.Now;
                    eventData.StatusId = (int)StatusEnum.Active;
                    this._eventDataRepository.Insert(eventData);
                }

            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(schedularData);
            }
        }

    }
}
