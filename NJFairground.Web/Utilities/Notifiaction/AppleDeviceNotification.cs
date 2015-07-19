
namespace NJFairground.Web.Utilities.Notifiaction
{
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using PushSharp;
    using PushSharp.Apple;
    using System;
    using System.IO;

    public class AppleDeviceNotification : DeviceNotification, IDeviceNotification
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
                var appleCert = File.ReadAllBytes(Path.Combine
                    (AppDomain.CurrentDomain.BaseDirectory, APNSCertificatePath));

                Broker.RegisterAppleService(new ApplePushChannelSettings
                    (!APNSUseSandBox, appleCert, APNSCertificatePassword, true));

                foreach (var device in AppleDevices)
                {
                    Broker.QueueNotification(new AppleNotification()
                        .ForDeviceToken(device.DeviceId)
                        .WithAlert(new AppleNotificationAlert() { LaunchImage = announcement.PageItemImageUrl, Body = announcement.PageHeaderText })
                        .WithCustomItem(DeviceNotification.PageItemId, announcement.PageItemId)
                        .WithCustomItem(DeviceNotification.NotificationToken, notificationToken)
                        .WithBadge(GetUnreadNotification(device))
                        .WithSound("default"));
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(notificationToken, announcement);
            }
        }
    }
}