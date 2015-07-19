
namespace NJFairground.Web.Utilities.Notifiaction
{
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Utilities.TaskScheduler.Hubs;
    using PushSharp;
    using PushSharp.Android;
    using PushSharp.Apple;
    using PushSharp.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class DeviceNotification
    {
        protected const string PageItemId = "PageItemId";
        protected const string NotificationToken = "NotificationToken";

        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly INotificationLogDataRepository _notificationLogDataRepository;
        private readonly IList<DeviceRegistryModel> _allDevices;
        private readonly PushBroker _broker;

        #region Public Properties
        /// <summary>
        /// Gets the broker.
        /// </summary>
        /// <value>
        /// The broker.
        /// </value>
        public PushBroker Broker { get { return this._broker; } }

        /// <summary>
        /// Gets the device registry data repository.
        /// </summary>
        /// <value>
        /// The device registry data repository.
        /// </value>
        public IDeviceRegistryDataRepository DeviceRegistryDataRepository
        {
            get { return _deviceRegistryDataRepository; }
        }

        /// <summary>
        /// Gets the notification log data repository.
        /// </summary>
        /// <value>
        /// The notification log data repository.
        /// </value>
        public INotificationLogDataRepository NotificationLogDataRepository
        {
            get { return _notificationLogDataRepository; }
        }

        /// <summary>
        /// Gets the android devices.
        /// </summary>
        /// <value>
        /// The android devices.
        /// </value>
        public IList<DeviceRegistryModel> AndroidDevices
        {
            get { return this._allDevices.Where(x => x.DeviceType.Equals((int)MobileDeviceType.Android)).ToList(); }
        }

        /// <summary>
        /// Gets the apple devices.
        /// </summary>
        /// <value>
        /// The apple devices.
        /// </value>
        public IList<DeviceRegistryModel> AppleDevices
        {
            get { return this._allDevices.Where(x => x.DeviceType.Equals((int)MobileDeviceType.Apple)).ToList(); }
        }

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

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceNotificationBase"/> class.
        /// </summary>
        /// <param name="deviceRegistryDataRepository">The device registry data repository.</param>
        /// <param name="notificationLogDataRepository">The notification log data repository.</param>
        public DeviceNotification(IDeviceRegistryDataRepository deviceRegistryDataRepository,
            INotificationLogDataRepository notificationLogDataRepository)
        {
            try
            {
                this._deviceRegistryDataRepository = deviceRegistryDataRepository;
                this._notificationLogDataRepository = notificationLogDataRepository;

                this._broker = new PushBroker();
                RegisterEvents();

                _allDevices = this._deviceRegistryDataRepository
                    .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceNotificationBase"/> class.
        /// </summary>
        public DeviceNotification()
            : this(ContainerProvider.Instance.GetInstance<IDeviceRegistryDataRepository>(),
            ContainerProvider.Instance.GetInstance<INotificationLogDataRepository>())
        {
        }
        #endregion

        #region Notification Events
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
        #endregion

        /// <summary>
        /// Gets the unread notification.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns></returns>
        public int GetUnreadNotification(DeviceRegistryModel device)
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

        #region Private Members
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
                else if (notification is AppleDeviceNotification)
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