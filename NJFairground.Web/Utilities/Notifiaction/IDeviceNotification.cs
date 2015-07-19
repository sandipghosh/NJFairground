
namespace NJFairground.Web.Utilities.Notifiaction
{
    using NJFairground.Web.Models;

    public interface IDeviceNotification
    {
        /// <summary>
        /// Notifies the specified notification token.
        /// </summary>
        /// <param name="notificationToken">The notification token.</param>
        /// <param name="announcement">The announcement.</param>
        void Notify(string notificationToken, PageItemModel announcement);
    }
}
