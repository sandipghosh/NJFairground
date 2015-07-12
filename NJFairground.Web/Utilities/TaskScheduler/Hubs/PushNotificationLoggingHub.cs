
namespace NJFairground.Web.Utilities.TaskScheduler.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using NJFairground.Web.Models;
    using System;
    using System.Linq;

    [HubName("PushNotificationLoggingHub")]
    public class PushNotificationLoggingHub : Hub
    {
        public PushNotificationLoggingHub()
        {
        }

        [HubMethodName("SubscribeLog")]
        public void SubscribeLog()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PushNotificationLoggingHub>();
            context.Clients.All.PushNotificationLog(NotificationType.info.ToString(), string.Format("{1} Notification Log Starts - {0:dd/MMM/yyyy} {1}",
                DateTime.Now.ToString("dd/MMM/yyyy"), string.Concat(Enumerable.Repeat("#", 10))));
        }
    }
}