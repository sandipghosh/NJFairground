
namespace NJFairground.Web.Utilities.Notifiaction
{
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using PushSharp;
    using PushSharp.Android;
    using System;

    public class AndroidDeviceNotification : DeviceNotification, IDeviceNotification
    {
        /// <summary>
        /// Notifies the specified notification token.
        /// </summary>
        /// <param name="notificationToken">The notification token.</param>
        /// <param name="announcement">The announcement.</param>
        public void Notify(string notificationToken, PageItemModel announcement)
        {
            try
            {
                if (!string.IsNullOrEmpty(GCMApiKey))
                {
                    Broker.RegisterGcmService(new GcmPushChannelSettings(this.GCMApiKey));

                    foreach (var device in AndroidDevices)
                    {
                        Broker.QueueNotification(new GcmNotification()
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
                ex.ExceptionValueTracker(notificationToken, announcement);
            }
        }
    }
}