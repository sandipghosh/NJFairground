﻿

namespace NJFairground.Web.Areas.Admin.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Filters;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    [AdminAuthentication]
    public class HomeController : Controller
    {
        private readonly IHitCounterDetailDataRepository _hitCounterDetailDataRepository;
        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly INotificationLogDataRepository _notificationLogDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="hitCounterDetailDataRepository">The hit counter detail data repository.</param>
        /// <param name="deviceRegistryDataRepository">The device registry data repository.</param>
        public HomeController(IHitCounterDetailDataRepository hitCounterDetailDataRepository,
            IDeviceRegistryDataRepository deviceRegistryDataRepository,
            INotificationLogDataRepository notificationLogDataRepository)
        {
            this._hitCounterDetailDataRepository = hitCounterDetailDataRepository;
            this._deviceRegistryDataRepository = deviceRegistryDataRepository;
            this._notificationLogDataRepository = notificationLogDataRepository;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DashboardViewModel dashboard = new DashboardViewModel();
            try
            {
                var totalHits = this._hitCounterDetailDataRepository.GetList().ToList();
                if (!totalHits.IsEmptyCollection())
                {
                    dashboard.TotalBannerHits = totalHits.Count(x => x.SponsorType.Equals("Banner"));
                    dashboard.TotalSplashHits = totalHits.Count(x => x.SponsorType.Equals("Splash"));
                }

                var registeredDevices = this._deviceRegistryDataRepository
                    .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                if (!registeredDevices.IsEmptyCollection())
                {
                    dashboard.TotalActiveiOSUsers = registeredDevices
                        .Count(x => x.DeviceType.Equals((int)MobileDeviceType.Apple));
                    dashboard.TotalActiveAndroidUsers = registeredDevices
                        .Count(x => x.DeviceType.Equals((int)MobileDeviceType.Android));
                }

                var notoificationLog = this._notificationLogDataRepository.GetNotificationLog();
                if (!notoificationLog.IsEmptyCollection())
                {
                    dashboard.NotificationLogDetail = notoificationLog;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return View(dashboard);
        }

        /// <summary>
        /// Gets the device pie.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDevicePie()
        {
            IList<DeviceViewModel> devices = new List<DeviceViewModel>();
            try
            {
                var registeredDevices = this._deviceRegistryDataRepository
                    .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                if (!registeredDevices.IsEmptyCollection())
                {
                    devices.Add(new DeviceViewModel
                    {
                        label = "iOS Users",
                        value = registeredDevices.Count(x => x.DeviceType.Equals((int)MobileDeviceType.Apple))
                    });
                    devices.Add(new DeviceViewModel
                    {
                        label = "Android Users",
                        value = registeredDevices.Count(x => x.DeviceType.Equals((int)MobileDeviceType.Android))
                    });
                }
                else
                {
                    devices.Add(new DeviceViewModel { label = "iOS Users", value = 0 });
                    devices.Add(new DeviceViewModel { label = "Android Users", value = 0 });
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return Json(devices, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the adds area chart.
        /// </summary>
        /// <param name="effectedDays">The effected days.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAddsAreaChart(int effectedDays, string type)
        {
            AreaChartViewModel payload = new AreaChartViewModel();
            try
            {
                //List<HitCounterDetailViewModel>
                Expression<Func<HitCounterDetailViewModel, bool>> filter = null;

                if (effectedDays > 0)
                {
                    DateTime lastDate = DateTime.Now.AddDays(-1 * effectedDays);
                    filter = (model) => (model.HitOn >= lastDate && model.HitOn <= DateTime.Now) && model.SponsorType.Equals(type);
                }
                else
                    filter = (model) => model.SponsorType.Equals(type);

                var totalHits = this._hitCounterDetailDataRepository.GetList(filter, x => x.HitOn, false).ToList();

                if (!totalHits.IsEmptyCollection())
                {
                    payload.labels = totalHits.Select(x => x.Name).Distinct().ToList();
                    var dataKeys = payload.labels.Select((x, i) => new { key = x, value = type + (i + 1) })
                        .ToDictionary(x => x.key, x => x.value);

                    payload.ykeys = dataKeys.Select(x => x.Value.ToString()).ToList();
                    payload.labels = dataKeys.Select(x => x.Key.ToString()).ToList();

                    var groupData = totalHits.GroupBy(x => new { x.Name, x.HitOn })
                        .Select(y => new { date = y.Key.HitOn.ToString("yyyy-MM-dd"), name = dataKeys[y.Key.Name], count = y.Count() });

                    var pivotData = groupData.Pivot(x => x.date, x => x.name, x => x.Sum(a => a.count).ToString());
                    foreach (var item in pivotData)
                        item.Value.Add("date", item.Key);

                    payload.xkey = "date";
                    payload.data = pivotData.Select(x => x.Value).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return new JSONActionResult(payload);
        }
    }

    public class DeviceViewModel
    {
        public string label { get; set; }
        public int value { get; set; }
    }

    public class AreaChartViewModel
    {
        public AreaChartViewModel()
        {
            this.data = new List<Dictionary<string, string>>();
            this.ykeys = new List<string>();
            this.labels = new List<string>();
        }

        public IList<Dictionary<string, string>> data { get; set; }
        public string xkey { get; set; }
        public IList<string> ykeys { get; set; }
        public IList<string> labels { get; set; }
    }
}