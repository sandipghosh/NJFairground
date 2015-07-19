

namespace NJFairground.Web.Utilities.TaskScheduler
{
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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
        private const string PageItemId = "PageItemId";
        private const string NotificationToken = "NotificationToken";
        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly INotificationLogDataRepository _notificationLogDataRepository;

        private readonly PushBroker _broker;
        private readonly Random _random;
        IList<DeviceRegistryModel> allDevices = new List<DeviceRegistryModel>();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        /// <param name="deviceRegistryDataRepository">The device registry data repository.</param>
        /// <param name="notificationLogDataRepository">The notification log data repository.</param>
        public PushNotificationEngine(IDeviceRegistryDataRepository deviceRegistryDataRepository,
            INotificationLogDataRepository notificationLogDataRepository)
        {
            this._deviceRegistryDataRepository = deviceRegistryDataRepository;
            this._notificationLogDataRepository = notificationLogDataRepository;

            this._broker = new PushBroker();
            this._random = new Random();

            RegisterEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationEngine"/> class.
        /// </summary>
        public PushNotificationEngine()
            : this(ContainerProvider.Instance.GetInstance<IDeviceRegistryDataRepository>(),
            ContainerProvider.Instance.GetInstance<INotificationLogDataRepository>())
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
        #endregion

        #region Public Properties
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
        /// Gets a value indicating whether this instance is technocal log.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is technocal log; otherwise, <c>false</c>.
        /// </value>
        public bool IsTechnocalLog { get { return CommonUtility.GetAppSetting<bool>("PN:IsTechnocalLog"); } }
        #endregion

        #region Notification Events Registration and Implementation
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
            try
            {
                if (this.DoLog)
                {
                    string msg = string.Format("Channel Destroyed For: {0}", sender);
                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.info, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender);
            }
        }

        /// <summary>
        /// Channels the created.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="pushChannel">The push channel.</param>
        private void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            try
            {
                if (this.DoLog)
                {
                    string msg = string.Format("Channel Created For: {0}", sender);
                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.info, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, pushChannel);
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
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Device Subscription Changed: {0} <br/> 
                            OldSubscriptionId: {1} <br/> NewSubscriptionId: {2} <br/>
                            Payload: {3}", sender, oldSubscriptionId, newSubscriptionId, notification);
                    else
                        msg = string.Format("Device Subscription Changed: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.info, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, oldSubscriptionId, newSubscriptionId, notification);
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
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Device Subscription Expired: {0} <br/> 
                            Expired Subscription Id: {1} <br/> Expiration Date Utc: {2:dd/MMM/yyyy} <br/> 
                            Payload: {3}", sender, expiredSubscriptionId, expirationDateUtc, notification);
                    else
                        msg = string.Format("Device Subscription Expired: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    RemoveNotificationLog(expiredSubscriptionId);
                    LogNotificationToClient(NotificationType.warning, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, expiredSubscriptionId, expirationDateUtc, notification);
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
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Notification Failed: {0} <br/> 
                            Device Id: {1} <br/> Exception Message: {2} <br/> 
                            Payload: {3}", sender, GetDeviceId(notification), error.Message, notification);
                    else
                        msg = string.Format("Notification Failed: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.danger, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, notification, error);
            }
        }

        /// <summary>
        /// Services the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="error">The error.</param>
        private void ServiceException(object sender, System.Exception error)
        {
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Service Exception: {0} <br/> Exception Message: {1}",
                            sender, error.Message);
                    else
                        msg = string.Format("Service Exception: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.danger, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, error);
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
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Channel Exception: {0} <br/> Exception Message: {1}",
                            sender, error.Message);
                    else
                        msg = string.Format("Channel Exception: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotificationToClient(NotificationType.danger, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, pushChannel, error);
            }
        }

        /// <summary>
        /// Notifications the sent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="notification">The notification.</param>
        private void NotificationSent(object sender, INotification notification)
        {
            try
            {
                if (this.DoLog)
                {
                    string msg = "";
                    if (this.IsTechnocalLog)
                        msg = string.Format(@"Notification Sent: {0} <br/> 
                            Device Id: {1} <br/>Payload: {2}", sender, GetDeviceId(notification), notification);
                    else
                        msg = string.Format("Notification Sent: {0}", sender);

                    //CommonUtility.LogToFileWithStack(msg);
                    LogNotification(notification);
                    LogNotificationToClient(NotificationType.success, msg);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(sender, notification);
            }
        }
        #endregion

        #region Send Notification
        /// <summary>
        /// Notifies the specified devices.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        public void Notify(PageItemModel announcement)
        {
            try
            {
                string notificationToken = Guid.NewGuid().ToString("N");
                Func<MobileDeviceType, List<DeviceRegistryModel>> devices = (type) =>
                    this.allDevices.Where(x => x.DeviceType.Equals((int)type)).ToList();

                IList<Task> notificationProcess = new List<Task>();

                notificationProcess.Add(Task.Factory.StartNew(() =>
                    NotifyToAndroid(devices(MobileDeviceType.Android), notificationToken, announcement)));

                notificationProcess.Add(Task.Factory.StartNew(() =>
                    NotifyToiOS(devices(MobileDeviceType.iOS), notificationToken, announcement)));

                Task.WaitAll(notificationProcess.ToArray());
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(announcement);
            }
        }

        /// <summary>
        /// Notifies the toi os.
        /// </summary>
        /// <param name="devices">The devices.</param>
        /// <param name="announcement">The announcement.</param>
        private void NotifyToiOS(IList<DeviceRegistryModel> devices, string notificationToken, PageItemModel announcement)
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
                        .WithCustomItem(PageItemId, announcement.PageItemId)
                        .WithCustomItem(NotificationToken, notificationToken)
                        .WithBadge(GetUnreadNotification(device))
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
        private void NotifyToAndroid(IList<DeviceRegistryModel> devices, string notificationToken, PageItemModel announcement)
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
                            .WithJson(Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                alert = announcement.PageHeaderText,
                                sound = "default",
                                badge = GetUnreadNotification(device),
                                LaunchImage = announcement.PageItemImageUrl,
                                PageItemId = announcement.PageItemId.ToString(),
                                NotificationToken = notificationToken
                            })));
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(devices, announcement);
            }
        }
        #endregion

        #region Notification Utility
        /// <summary>
        /// Logs the notification to client.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
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

        /// <summary>
        /// Logs the notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        private void LogNotification(INotification notification)
        {
            try
            {
                Func<AppleNotificationPayload, string, string> parseApplePayload
                    = (payload, key) => payload.CustomItems[key].FirstOrDefault().ToString();

                Func<string, string, string> parseAndroidPayload = (payload, key) =>
                {
                    var data = (JObject)JsonConvert.DeserializeObject(payload);
                    return data[key].Value<string>();
                };

                NotificationLogModel notificationLog = new NotificationLogModel();
                if (notification is GcmNotification)
                {
                    GcmNotification gcmNotification = (GcmNotification)notification;
                    notificationLog.DeviceId = gcmNotification.RegistrationIds.FirstOrDefault();
                    notificationLog.NotifiactionToken = parseAndroidPayload(gcmNotification.JsonData, NotificationToken);
                    notificationLog.PageItemId = int.Parse(parseAndroidPayload(gcmNotification.JsonData, PageItemId));
                }
                else if (notification is AppleNotification)
                {
                    AppleNotification appleNotification = (AppleNotification)notification;
                    notificationLog.DeviceId = appleNotification.DeviceToken;
                    notificationLog.NotifiactionToken = parseApplePayload(appleNotification.Payload, NotificationToken);
                    notificationLog.PageItemId = int.Parse(parseApplePayload(appleNotification.Payload, PageItemId));
                }

                this._notificationLogDataRepository.InsertNotificationLog(notificationLog);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(notification);
            }
        }

        /// <summary>
        /// Gets the unread notification.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        private int GetUnreadNotification(DeviceRegistryModel device)
        {
            int result = 1;
            try
            {
                if (!device.NotificationLogs.IsEmptyCollection())
                {
                    result = device.NotificationLogs.Count
                        (x => !x.IsRead && x.StatusId.Equals((int)StatusEnum.Active));

                    result++;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(device);
            }
            return result;
        }

        /// <summary>
        /// Removes the notification log.
        /// </summary>
        /// <param name="oldDeviceId">The old device identifier.</param>
        private void RemoveNotificationLog(string oldDeviceId)
        {
            try
            {
                DeviceRegistryModel device = this._deviceRegistryDataRepository.GetList
                    (x => x.DeviceId.Equals(oldDeviceId) && x.StatusId.Equals((int)StatusEnum.Active))
                    .FirstOrDefaultCustom();

                if (device != null)
                {
                    device.StatusId = (int)StatusEnum.Inactive;
                    device.UpdatedOn = DateTime.Now;
                    this._deviceRegistryDataRepository.Update(device);

                    int registeredDeviceId = device.DeviceRegistryId;
                    var notificationLogs = this._notificationLogDataRepository.GetList
                        (x => x.DeviceRegistryId.Equals(registeredDeviceId) && x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                    notificationLogs.ForEach(x =>
                    {
                        x.StatusId = (int)StatusEnum.Inactive;
                    });
                    this._notificationLogDataRepository.Update(notificationLogs);
                }

            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(oldDeviceId);
            }
        }

        /// <summary>
        /// Gets the device identifier.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        private string GetDeviceId(INotification notification)
        {
            try
            {
                if (notification is GcmNotification)
                {
                    GcmNotification gcmNotification = (GcmNotification)notification;
                    return gcmNotification.RegistrationIds.FirstOrDefault();
                }
                else if (notification is AppleNotification)
                {
                    AppleNotification appleNotification = (AppleNotification)notification;
                    return appleNotification.DeviceToken;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(notification);
            }
            return string.Empty;
        }
        #endregion
    }
}