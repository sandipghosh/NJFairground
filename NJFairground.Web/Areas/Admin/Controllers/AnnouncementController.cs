

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Utilities.TaskScheduler;
    using NJFairground.Web.Utilities.Notifiaction;
    using NJFairground.Web.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AnnouncementController : Controller
    {
        private readonly IPageItemDataRepository _pageItemDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
        /// </summary>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        public AnnouncementController(IPageItemDataRepository pageItemDataRepository)
        {
            this._pageItemDataRepository = pageItemDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Index()
        {
            try
            {
                var announcements = this._pageItemDataRepository.GetList(x => x.PageId.Equals(CommonUtility.GetAppSetting<int>("AnnouncementId"))
                    && x.StatusId.Equals((int)StatusEnum.Active), x => x.ItemOrder, true).ToList();

                if (!announcements.IsEmptyCollection())
                {
                    return View(announcements);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Content("");
        }

        /// <summary>
        /// Notifies the specified page item identifier.
        /// </summary>
        /// <param name="pageItemId">The page item identifier.</param>
        /// <returns></returns>
        [HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByHeader = "*")]
        public ActionResult Notify(int pageItemId)
        {
            try
            {
                PushNotificationEngine pushNotificationEngine = new PushNotificationEngine();
                var announcement = this._pageItemDataRepository.Get(pageItemId);
                if (announcement != null)
                {
                    string notificationToken = Guid.NewGuid().ToString("N");
                    IList<Task> notificationProcess = new List<Task>();

                    notificationProcess.Add(Task.Factory.StartNew(() =>
                        DeviceNotificationService.GetDeviceNotification(MobileDeviceType.Apple)
                        .Notify(notificationToken, announcement)));

                    notificationProcess.Add(Task.Factory.StartNew(() =>
                        DeviceNotificationService.GetDeviceNotification(MobileDeviceType.Android)
                        .Notify(notificationToken, announcement)));

                    Task.WaitAll(notificationProcess.ToArray());



                    //pushNotificationEngine.Notify(announcement);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(pageItemId);
            }
            return Content("notified");
        }
    }
}