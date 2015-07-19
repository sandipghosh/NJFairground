
namespace NJFairground.Web.Utilities.Notifiaction
{
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;

    public class DeviceNotificationService
    {
        /// <summary>
        /// Gets the device notification.
        /// </summary>
        /// <param name="announcement">The announcement.</param>
        /// <param name="deviceType">Type of the device.</param>
        /// <returns></returns>
        public static IDeviceNotification GetDeviceNotification(MobileDeviceType deviceType)
        {
            IDeviceNotification deviceNotifiaction = null;
            try
            {
                switch (deviceType)
                {
                    case MobileDeviceType.Apple:
                        deviceNotifiaction = new AppleDeviceNotification();
                        break;
                    case MobileDeviceType.Android:
                        deviceNotifiaction = new AndroidDeviceNotification();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(deviceType);
            }
            return deviceNotifiaction;
        }
    }
}