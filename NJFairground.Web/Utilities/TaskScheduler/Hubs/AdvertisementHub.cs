

namespace NJFairground.Web.Utilities.TaskScheduler.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Models;

    [HubName("AdvertisementHub")]
    public class AdvertisementHub : Hub
    {
        private readonly IBannerDataRepository _bannerDataRepository;
        private readonly IList<BannerItemModel> _adds;
        static Random rnd = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementHub"/> class.
        /// </summary>
        /// <param name="bannerDataRepository">The banner data repository.</param>
        public AdvertisementHub(IBannerDataRepository bannerDataRepository)
        {
            this._bannerDataRepository = bannerDataRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementHub"/> class.
        /// </summary>
        public AdvertisementHub()
            : this(new BannerDataRepository(new Data.Implementation.Base.UnitOfWork
                <Data.Context.NJFairgroundDBEntities>()))
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

        /// <summary>
        /// Pop-ups the advertisement splash.
        /// </summary>
        [HubMethodName("PopupAdvertisementSplash")]
        public void PopupAdvertisementSplash()
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
    }
}