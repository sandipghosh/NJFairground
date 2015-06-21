using Microsoft.Owin;
using NJFairground.Web.Utilities.TaskScheduler;

[assembly: OwinStartup(typeof(NotificationEngine))]
namespace NJFairground.Web.Utilities.TaskScheduler
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;

    public class NotificationEngine
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(new HubConfiguration { EnableDetailedErrors = true, EnableJSONP = true });
        }
    }
}