using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJFairground.Web.Utilities.TaskScheduler.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;

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
            context.Clients.All.PushNotificationLog(string.Format("{1} Notification Log Starts - {0:dd/MMM/yyyy} {1}", 
                DateTime.Now.ToString("dd/MMM/yyyy"), Enumerable.Repeat("#", 10)));
        }
    }
}