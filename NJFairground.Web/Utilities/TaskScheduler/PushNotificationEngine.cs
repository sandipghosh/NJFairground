

namespace NJFairground.Web.Utilities.TaskScheduler
{
    using Microsoft.AspNet.SignalR;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities.TaskScheduler.Hubs;
    using PushSharp;
    using PushSharp.Android;
    using PushSharp.Apple;
    using PushSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PushNotificationEngine
    {
        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly PushBroker _broker;
        private readonly Random _random;
        IList<DeviceRegistryModel> allDevices = new List<DeviceRegistryModel>();

        /// <summary>
        /// Gets a value indicating whether [do log].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [do log]; otherwise, <c>false</c>.
        /// </value>
        public bool DoLog { get { return CommonUtility.GetAppSetting<bool>("PN:DoLog"); } }
        /// <summary>
        /// Gets a value indicating whether [apns use sand box].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [apns use sand box]; otherwise, <c>false</c>.
        /// </value>
        public bool APNSUseSandBox { get { return CommonUtility.GetAppSetting<bool>("iOS:UseSandBox"); } }
        /// <summary>
        /// Gets a value indicating whether [apns certificate path].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [apns certificate path]; otherwise, <c>false</c>.
        /// </value>
        public string APNSCertificatePath
        {
            get
            {
                if (APNSUseSandBox)
                    return CommonUtility.GetAppSetting<string>("iOS:CertificateSandbox");
                else
                    return CommonUtility.GetAppSetting<string>("iOS:CertificateProduction");
            }
        }
        /// <summary>
        /// Gets a value indicating whether [apns certificate password].
        /// </summary>
        /// <value>
        /// <c>true</c> if [apns certificate password]; otherwise, <c>false</c>.
        /// </value>
        public string APNSCertificatePassword { get { return CommonUtility.GetAppSetting<string>("iOS:CertificatePassword"); } }
        /// <summary>
        /// Gets a value indicating whether [GCM API key].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [GCM API key]; otherwise, <c>false</c>.
        /// </value>
        public string GCMApiKey { get { return CommonUtility.GetAppSetting<string>("Android:ApiKey"); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        /// <param name="deviceRegistryDataRepository">The device registry data repository.</param>
        public PushNotificationEngine(IDeviceRegistryDataRepository deviceRegistryDataRepository)
        {
            this._deviceRegistryDataRepository = deviceRegistryDataRepository;
            this._broker = new PushBroker();
            this._random = new Random();
            RegisterEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        public PushNotificationEngine()
            : this(ContainerProvider.Instance.GetInstance<IDeviceRegistryDataRepository>())
        {
            try
            {
                allDevices = this._deviceRegistryDataRepository
                    .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Notifies the specified devices.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        public void Notify(PageItemModel announcement)
        {
            try
            {
                Func<MobileDeviceType, List<DeviceRegistryModel>> devices = (type) =>
                    this.allDevices.Where(x => x.DeviceType.Equals((int)type)).ToList();

                IList<Task> notificationProcess = new List<Task>();
                notificationProcess.Add(Task.Factory.StartNew(() =>
                    NotifyToAndroid(devices(MobileDeviceType.Android), announcement)));

                notificationProcess.Add(Task.Factory.StartNew(() =>
                    NotifyToiOS(devices(MobileDeviceType.iOS), announcement)));

                Task.WaitAll(notificationProcess.ToArray());
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(announcement);
            }
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
            if (this.DoLog)
            {
                string msg = string.Format("Channel Destroyed For: {0}", sender);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.info, msg);
            }
        }

        /// <summary>
        /// Channels the created.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pushChannel">The push channel.</param>
        private void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            if (this.DoLog)
            {
                string msg = string.Format("Channel Created For: {0}", sender);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.info, msg);
            }
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
            if (this.DoLog)
            {
                string msg = string.Format("Device Subscription Changed: {0} -> {1} -> {2} -> {3}",
                    sender, oldSubscriptionId, newSubscriptionId, notification);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.info, msg);
            }
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
            if (this.DoLog)
            {
                string msg = string.Format("Device Subscription Expired: {0} -> {1} -> {2} -> {3}",
                    sender, expiredSubscriptionId, expirationDateUtc, notification);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.warning, msg);
            }
        }

        /// <summary>
        /// Notifications the failed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="error">The error.</param>
        private void NotificationFailed(object sender, INotification notification, System.Exception error)
        {
            if (this.DoLog)
            {
                string msg = string.Format("Failure: {0} -> {1} -> {2}", sender, error.Message, notification);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.danger, msg);
            }
        }

        /// <summary>
        /// Services the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="error">The error.</param>
        private void ServiceException(object sender, System.Exception error)
        {
            if (this.DoLog)
            {
                string msg = string.Format("Service Exception: {0} -> {1}", sender, error.Message);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.danger, msg);
            }
        }

        /// <summary>
        /// Channels the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pushChannel">The push channel.</param>
        /// <param name="error">The error.</param>
        private void ChannelException(object sender, IPushChannel pushChannel, System.Exception error)
        {
            if (this.DoLog)
            {
                string msg = string.Format("Channel Exception: {0} -> {1}", sender, error.Message);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.danger, msg);
            }
        }

        /// <summary>
        /// Notifications the sent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="notification">The notification.</param>
        private void NotificationSent(object sender, INotification notification)
        {
            if (this.DoLog)
            {
                string msg = string.Format("Sent: {0} -> {1}", sender, notification);
                //CommonUtility.LogToFileWithStack(msg);
                LogNotificationToClient(NotificationType.success, msg);
            }
        }

        /// <summary>
        /// Notifies the toi os.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        private void NotifyToiOS(IList<DeviceRegistryModel> devices, PageItemModel announcement)
        {
            try
            {
                var appleCert = File.ReadAllBytes(Path.Combine
                        (AppDomain.CurrentDomain.BaseDirectory, this.APNSCertificatePath));

                this._broker.RegisterAppleService(new ApplePushChannelSettings
                    (!this.APNSUseSandBox, appleCert, this.APNSCertificatePassword, true));

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
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(devices, announcement);
            }
        }

        /// <summary>
        /// Notifies to android.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        private void NotifyToAndroid(IList<DeviceRegistryModel> devices, PageItemModel announcement)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.GCMApiKey))
                {
                    _broker.RegisterGcmService(new GcmPushChannelSettings(this.GCMApiKey));

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
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(devices, announcement);
            }
        }

        private void LogNotificationToClient(NotificationType messageType, string message)
        {
            try
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PushNotificationLoggingHub>();
                context.Clients.All.PushNotificationLog(messageType.ToString(), message);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(messageType, message);
            }
        }
    }
}