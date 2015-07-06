

namespace NJFairground.Web.Utilities.TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using PushSharp;
    using PushSharp.Android;
    using PushSharp.Apple;
    using PushSharp.Core;

    public class PushNotificationEngine
    {
        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly PushBroker _broker;
        IList<DeviceRegistryModel> allDevices = new List<DeviceRegistryModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        /// <param name="deviceRegistryDataRepository">The device registry data repository.</param>
        public PushNotificationEngine(IDeviceRegistryDataRepository deviceRegistryDataRepository)
        {
            this._deviceRegistryDataRepository = deviceRegistryDataRepository;
            this._broker = new PushBroker();
            RegisterEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        public PushNotificationEngine()
            : this((new SimpleInjector.Container()).GetInstance<IDeviceRegistryDataRepository>())
        {
            allDevices = this._deviceRegistryDataRepository
                .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
        }

        /// <summary>
        /// Notifies the specified devices.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        public void Notify(IList<DeviceRegistryModel> devices, PageItemModel announcement)
        {
            Task[] notificationProcess = new Task[] { 
                new Task(()=>NotifyToAndroid(allDevices.Where(x=>x.DeviceType.Equals((int)MobileDeviceType.Android)).ToList(), announcement)),
                new Task(()=>NotifyToiOS(allDevices.Where(x=>x.DeviceType.Equals((int)MobileDeviceType.iOS)).ToList(),announcement))
            };

            Task.WaitAll(notificationProcess);
        }

        /// <summary>
        /// Registers the events.
        /// </summary>
        private void RegisterEvents()
        {
            _broker.OnNotificationSent += NotificationSent;
            _broker.OnChannelException += ChannelException;
            _broker.OnServiceException += ServiceException;
            _broker.OnNotificationFailed += NotificationFailed;
            _broker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            _broker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            _broker.OnChannelCreated += ChannelCreated;
            _broker.OnChannelDestroyed += ChannelDestroyed;
        }

        /// <summary>
        /// Channels the destroyed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ChannelDestroyed(object sender)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Channels the created.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pushChannel">The push channel.</param>
        private void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Devices the subscription changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="oldSubscriptionId">The old subscription identifier.</param>
        /// <param name="newSubscriptionId">The new subscription identifier.</param>
        /// <param name="notification">The notification.</param>
        private void DeviceSubscriptionChanged(object sender, string oldSubscriptionId,
            string newSubscriptionId, INotification notification)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Devices the subscription expired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="expiredSubscriptionId">The expired subscription identifier.</param>
        /// <param name="expirationDateUtc">The expiration date UTC.</param>
        /// <param name="notification">The notification.</param>
        private void DeviceSubscriptionExpired(object sender, string expiredSubscriptionId,
            System.DateTime expirationDateUtc, INotification notification)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Notifications the failed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="error">The error.</param>
        private void NotificationFailed(object sender, INotification notification, System.Exception error)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Services the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="error">The error.</param>
        private void ServiceException(object sender, System.Exception error)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Channels the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pushChannel">The push channel.</param>
        /// <param name="error">The error.</param>
        private void ChannelException(object sender, IPushChannel pushChannel, System.Exception error)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Notifications the sent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="notification">The notification.</param>
        private void NotificationSent(object sender, INotification notification)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Notifies the toi os.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        private void NotifyToiOS(IList<DeviceRegistryModel> devices, PageItemModel announcement)
        {
            var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                CommonUtility.GetAppSetting<string>("iOS:CertificateFilePath")));
            _broker.RegisterAppleService(new ApplePushChannelSettings(true, appleCert,
                CommonUtility.GetAppSetting<string>("iOS:CertificatePassword")));

            foreach (var device in devices)
            {
                _broker.QueueNotification(new AppleNotification()
                    .ForDeviceToken(device.DeviceId)
                    .WithAlert(new AppleNotificationAlert() { LaunchImage = announcement.PageItemImageUrl, Body = announcement.PageHeaderText })
                    .WithCustomItem("PageItemId", announcement.PageItemId)
                    .WithBadge(7)
                    .WithSound("default"));
            }
        }

        /// <summary>
        /// Notifies to android.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        private void NotifyToAndroid(IList<DeviceRegistryModel> devices, PageItemModel announcement)
        {
            _broker.RegisterGcmService(new GcmPushChannelSettings
                (CommonUtility.GetAppSetting<string>("Android:ApiKey")));

            foreach (var device in devices)
            {
                _broker.QueueNotification(new GcmNotification()
                    .ForDeviceRegistrationId(device.DeviceId)
                    .WithData(new Dictionary<string, string>() { { "PageItemId", announcement.PageItemId.ToString() } })
                    .WithJson(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        alert = announcement.PageHeaderText,
                        sound = "default",
                        badge = 7
                    })));
            }
        }
    }
}