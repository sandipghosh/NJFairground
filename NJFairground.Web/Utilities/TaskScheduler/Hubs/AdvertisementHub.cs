
namespace NJFairground.Web.Utilities.TaskScheduler.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;
    using SimpleInjector;

    [HubName("AdvertisementHub")]
    public class AdvertisementHub : Hub
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        private IList<BannerItemModel> _adds;
        static Random rnd = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementHub"/> class.
        /// </summary>
        public AdvertisementHub()
        {
            Container container = new Container();
            this._bannerDataRepository = container.GetInstance<BannerDataRepository>();
            InitiateAdvertisementSplashImages();

            var periodicalBroadcast = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    BroadcastAdvertisementSplash();
                    await Task.Delay(3000);
                }
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Pop-ups the advertisement splash.
        /// </summary>
        [HubMethodName("PopupAdvertisementSplash")]
        public void PopupAdvertisementSplash()
        {
            try
            {
                BroadcastAdvertisementSplash();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Broadcasts the advertisement splash.
        /// </summary>
        private void BroadcastAdvertisementSplash()
        {
            try
            {
                int index = rnd.Next(this._adds.Count);
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AdvertisementHub>();
                context.Clients.All.BroadcastAdds(JsonConvert.SerializeObject(this._adds[index]));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }

        /// <summary>
        /// Initiates the advertisement splash images.
        /// </summary>
        private void InitiateAdvertisementSplashImages()
        {
            try
            {
                var splash = this._bannerDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                && x.IsDefault == true && x.IsSplashImage == true).FirstOrDefault();

                if (!splash.BannerItems.IsEmptyCollection())
                {
                    this._adds = splash.BannerItems;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
        }
    }
}