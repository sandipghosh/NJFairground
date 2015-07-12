
namespace NJFairground.Web.Data.Interface
{
    using NJFairground.Web.Data.Interface.Base;
    using NJFairground.Web.Models;

    public interface INotificationLogDataRepository : IRepository<NotificationLogModel>
    {
        /// <summary>
        /// Inserts the notification log.
        /// </summary>
        /// <param name="notificationLog">The notification log.</param>
        /// <returns></returns>
        int InsertNotificationLog(NotificationLogModel notificationLog);

        /// <summary>
        /// Marks the read notification log.
        /// </summary>
        /// <param name="notificationLog">The notification log.</param>
        /// <returns></returns>
        int MarkReadNotificationLog(NotificationLogModel notificationLog);
    }
}
