
namespace NJFairground.Web.Data.Implementation
{
    using System;
    using NJFairground.Web.Data.Context;
    using NJFairground.Web.Data.Implementation.Base;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Data.Interface.Base;
    using System.Data.SqlClient;

    public class NotificationLogDataRepository
        : DataRepository<NotificationLog, NotificationLogModel>, INotificationLogDataRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationLogDataRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public NotificationLogDataRepository(UnitOfWork<NJFairgroundDBEntities> unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Initializes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private SqlParameter InitParam<T>(string paramName, T input)
        {
            SqlParameter parameter;
            if (input == null)
                parameter = new SqlParameter(paramName, DBNull.Value);
            else
                parameter = new SqlParameter(paramName, input);

            return parameter;
        }

        /// <summary>
        /// Inserts the notification log.
        /// </summary>
        /// <param name="notificationLog">The notification log.</param>
        /// <returns></returns>
        public int InsertNotificationLog(NotificationLogModel notificationLog)
        {
            int result = 0;
            try
            {
                IQueryDataRepository query = new QueryDataRepository<NJFairgroundDBEntities>();
                SqlParameter[] param = new SqlParameter[]
                {
                    InitParam("@NotifiactionToken", notificationLog.NotifiactionToken),
                    InitParam("@DeviceId", notificationLog.DeviceId),
                    InitParam("@PageItemId", notificationLog.PageItemId)
                };

                result = query.ExecuteCommand("EXEC InsertNotificationLog @NotifiactionToken, @DeviceId, @PageItemId", param);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(notificationLog);
            }
            return result;
        }

        /// <summary>
        /// Marks the read notification log.
        /// </summary>
        /// <param name="notificationLog">The notification log.</param>
        /// <returns></returns>
        public int MarkReadNotificationLog(NotificationLogModel notificationLog) 
        {
            int result = 0;
            try
            {
                IQueryDataRepository query = new QueryDataRepository<NJFairgroundDBEntities>();
                SqlParameter[] param = new SqlParameter[]
                {
                    InitParam("@NotifiactionToken", notificationLog.NotifiactionToken),
                    InitParam("@DeviceId", notificationLog.DeviceId),
                };

                result = query.ExecuteCommand("EXEC MarkReadNotificationLog @NotifiactionToken, @DeviceId", param);
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(notificationLog);
            }
            return result;
        }
    }
}