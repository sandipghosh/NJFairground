

namespace NJFairground.Web.Utilities.TaskScheduler.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;

    [HubName("AnnouncementHub")]
    public class AnnouncementHub : Hub
    {
        public AnnouncementHub()
        {
        }

        [HubMethodName("SubscribeAnnouncement")]
        public void SubscribeAnnouncement()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AnnouncementHub>();
            context.Clients.All.GetAnnouncements(DateTime.Now.ToString("dd/MMM/yyyy"), "Subscription Made");
        }
    }
}