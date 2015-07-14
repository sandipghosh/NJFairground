

namespace NJFairground.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.ServiceModel.Syndication;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;
    using System.Xml.Linq;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.DTO.RequestDto;
    using NJFairground.Web.DTO.ResponseDto;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using Newtonsoft.Json.Linq;

    public class PageApiController : ApiController
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;
        private readonly IUserInfoDataRepository _userInfoDataRepository;
        private readonly IFavoritePageDataRepository _favoritePageDataRepository;
        private readonly IFavoriteImageDataRepository _favoriteImageDataRepository;
        private readonly IUserImageDataRepository _userImageDataRepository;
        private readonly IBannerDataRepository _bannerDataRepository;
        private readonly IPageBannerDataRepository _pageBannerDataRepository;
        private readonly ISplashImageDataRepository _splashImageDataRepository;
        private readonly IHitCounterDataRepository _hitCounterDataRepository;
        private readonly IDeviceRegistryDataRepository _deviceRegistryDataRepository;
        private readonly INotificationLogDataRepository _notificationLogDataRepository;

        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageApiController"/> class.
        /// </summary>
        /// <param name="pageDataRepository">The page data repository.</param>
        /// <param name="pageItemDataRepository">The page item data repository.</param>
        /// <param name="userInfoDataRepository">The user information data repository.</param>
        /// <param name="favoritePageDataRepository">The favorite page data repository.</param>
        /// <param name="favoriteImageDataRepository">The favorite image data repository.</param>
        /// <param name="userImageDataRepository">The user image data repository.</param>
        /// <param name="bannerDataRepository">The banner data repository.</param>
        /// <param name="pageBannerDataRepository">The page banner data repository.</param>
        public PageApiController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository,
            IUserInfoDataRepository userInfoDataRepository,
            IFavoritePageDataRepository favoritePageDataRepository,
            IFavoriteImageDataRepository favoriteImageDataRepository,
            IUserImageDataRepository userImageDataRepository,
            IBannerDataRepository bannerDataRepository,
            IPageBannerDataRepository pageBannerDataRepository,
            ISplashImageDataRepository splashImageDataRepository,
            IHitCounterDataRepository hitCounterDataRepository,
            IDeviceRegistryDataRepository deviceRegistryDataRepository,
            INotificationLogDataRepository notificationLogDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
            this._userInfoDataRepository = userInfoDataRepository;
            this._favoritePageDataRepository = favoritePageDataRepository;
            this._favoriteImageDataRepository = favoriteImageDataRepository;
            this._userImageDataRepository = userImageDataRepository;
            this._bannerDataRepository = bannerDataRepository;
            this._pageBannerDataRepository = pageBannerDataRepository;
            this._splashImageDataRepository = splashImageDataRepository;
            this._hitCounterDataRepository = hitCounterDataRepository;
            this._deviceRegistryDataRepository = deviceRegistryDataRepository;
            this._notificationLogDataRepository = notificationLogDataRepository;
        }

        /// <summary>
        /// Registers the device.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public DeviceRegistryResponseDto RegisterDevice(DeviceRegistryRequestDto request)
        {
            DeviceRegistryResponseDto response = InitiateResponse<DeviceRegistryRequestDto, DeviceRegistryResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Insert)
                {
                    DeviceRegistryModel registeredDevice = this._deviceRegistryDataRepository
                        .GetList(x => x.DeviceId.Equals(request.DeviceId) && x.DeviceType.Equals(request.DeciceType)
                        && x.StatusId.Equals((int)StatusEnum.Active)).FirstOrDefault();

                    if (registeredDevice == null)
                    {
                        registeredDevice = new DeviceRegistryModel
                        {
                            DeviceId = request.DeviceId,
                            DeviceType = request.DeciceType,
                            StatusId = (int)StatusEnum.Active,
                            CreatedOn = DateTime.Now
                        };

                        this._deviceRegistryDataRepository.Insert(registeredDevice);
                        if (registeredDevice.DeviceRegistryId > 0)
                            response.Device = registeredDevice;
                    }
                    else
                        response.Device = registeredDevice;
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the application information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public AppInfoResponseDto GetAppInfo(AppInfoRequestDto request)
        {
            AppInfoResponseDto response = InitiateResponse<AppInfoRequestDto, AppInfoResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Select)
                {
                    AppInfoModel appInfo = new AppInfoModel
                    {
                        FairName = CommonUtility.GetAppSetting<string>("FairName"),
                        FairYear = CommonUtility.GetAppSetting<string>("FairYear"),
                        FairVenue = CommonUtility.GetAppSetting<string>("FairVenue"),
                        FairCatchline = CommonUtility.GetAppSetting<string>("FairCatchline"),
                        FairSubname = CommonUtility.GetAppSetting<string>("FairSubname")
                    };
                    response.AppInfo = appInfo;
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public PageResponseDto GetPage(PageRequestDto request)
        {
            PageResponseDto response = InitiateResponse<PageRequestDto, PageResponseDto>(request);
            try
            {
                if (request.PageId > 0 && request.Action == CrudAction.Select)
                {
                    response.Page = this._pageDataRepository.Get(request.PageId);
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
                else if (request.Action == CrudAction.BulkSelect)
                {
                    if (!string.IsNullOrEmpty(request.Filter))
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.Pages = this._pageDataRepository
                                .GetList(request.ItemIndex, request.ItemCount, request.GetExpression<PageModel>()).ToList();
                        else
                            response.Pages = this._pageDataRepository
                                .GetList(request.GetExpression<PageModel>()).ToList();

                        response.ResponseStatus = RespStatus.Success.ToString();
                    }
                    else
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.Pages = this._pageDataRepository
                                .GetList(request.ItemIndex, request.ItemCount,
                                x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                        else
                            response.Pages = this._pageDataRepository
                                .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                        response.ResponseStatus = RespStatus.Success.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the page item.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public PageItemResponseDto GetPageItem(PageItemRequestDto request)
        {
            PageItemResponseDto response = InitiateResponse<PageItemRequestDto, PageItemResponseDto>(request);
            try
            {
                if (request.PageItemId > 0)
                {
                    response.PageItem = this._pageItemDataRepository.Get(request.PageItemId);
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
                else if (request.PageId > 0 && request.Action == CrudAction.BulkSelect)
                {
                    if (request.ItemCount > 0 && request.ItemIndex > 0)
                        response.PageItems = this._pageItemDataRepository.GetList(request.ItemIndex, request.ItemCount,
                            x => x.StatusId.Equals((int)StatusEnum.Active) && x.PageId.Equals(request.PageId), x => x.ItemOrder, true).ToList();
                    else
                        response.PageItems = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageId.Equals(request.PageId), x => x.ItemOrder, true).ToList();

                    response.ResponseStatus = RespStatus.Success.ToString();
                }
                else if (request.Action == CrudAction.BulkSelect)
                {
                    if (!string.IsNullOrEmpty(request.Filter))
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.PageItems = this._pageItemDataRepository.GetList(request.ItemIndex,
                                request.ItemCount, request.GetExpression<PageItemModel>(), x => x.ItemOrder, true).ToList();
                        else
                            response.PageItems = this._pageItemDataRepository
                                .GetList(request.GetExpression<PageItemModel>(), x => x.ItemOrder, true).ToList();
                    }
                    else
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.PageItems = this._pageItemDataRepository.GetList(request.ItemIndex, request.ItemCount,
                                x => x.StatusId.Equals((int)StatusEnum.Active), x => x.ItemOrder, true).ToList();
                        else
                            response.PageItems = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active),
                                x => x.ItemOrder, true).ToList();
                    }
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public MapResponseDto GetMap(MapRequestDto request)
        {
            MapResponseDto response = new MapResponseDto(request.RequestToken);
            response.ResponseStatus = RespStatus.Success.ToString();
            return response;
        }

        /// <summary>
        /// Gets the media feed.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public RssFeedResponseDto GetMediaFeed(RssFeedRequestDto request)
        {
            RssFeedResponseDto response = InitiateResponse<RssFeedRequestDto, RssFeedResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.BulkSelect)
                {
                    string feedLink = "";
                    switch (request.FeedRequestFor)
                    {
                        case FeedFor.Facebook:
                            feedLink = "Facebook:RssFeed";
                            break;
                        case FeedFor.Twitter:
                            feedLink = "Twitter:RssFeed";
                            break;
                        case FeedFor.Instagram:
                            feedLink = "Instagram:RssFeed";
                            break;
                        case FeedFor.Pinterest:
                            feedLink = "Pinterest:RssFeed";
                            break;
                    }
                    string rssFeedAsString = string.Empty;

                    switch (request.FeedRequestFor)
                    {
                        case FeedFor.Facebook:
                            {
                                rssFeedAsString = CommonUtility.GetFacebookJsonFeedAsString();
                                if (!string.IsNullOrEmpty(rssFeedAsString))
                                {
                                    JObject jsonFeed = JObject.Parse(rssFeedAsString);
                                    response.SocialFeeds = jsonFeed["data"].Select(x => new RssFeedModel
                                    {
                                        Title = (x["message"].AsString().Length > 20) ?
                                            x["message"].AsString().Substring(0, 20) + ".." : x["message"].AsString(),
                                        TitleUrl = "",
                                        ImageLink = x["picture"].AsString(),
                                        ImageUrl = x["link"].AsString(),
                                        Content = x["message"].AsString(),
                                        LastUpdate = (x["updated_time"] ?? x["created_time"]).AsString(),
                                        Author = (x["from"] != null) ? x["from"]["name"].AsString() : ""
                                    }).ToList();
                                }
                                break;
                            }
                        case FeedFor.Twitter:
                        case FeedFor.Pinterest:
                            {
                                rssFeedAsString = CommonUtility.GetRSSFeedAsString(feedLink);
                                if (!string.IsNullOrEmpty(rssFeedAsString))
                                {
                                    SyndicationFeed feed = SyndicationFeed.Load(XDocument.Parse(rssFeedAsString).CreateReader());
                                    response.SocialFeeds = feed.Items.Select(x => new RssFeedModel
                                    {
                                        Title = x.Title.Text,
                                        TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                                        Content = ((TextSyndicationContent)(x.Content ?? x.Summary)).Text,
                                        LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                                            x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                                            x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                                        Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.ToString()
                                    }).ToList();
                                }

                                break;
                            }
                        case FeedFor.Instagram:
                            {
                                rssFeedAsString = CommonUtility.GetRSSFeedAsString(feedLink);
                                if (!string.IsNullOrEmpty(rssFeedAsString))
                                {
                                    XDocument doc = XDocument.Parse(rssFeedAsString);
                                    response.SocialFeeds = doc.Descendants("item").Select(x => new RssFeedModel
                                    {
                                        Title = x.Element("title").Value.ToString(),
                                        TitleUrl = x.Element("link").Value.ToString(),
                                        ImageLink = x.Element("image").Element("link").Value.ToString(),
                                        ImageUrl = x.Element("image").Element("link").Value.ToString(),
                                        Content = x.Element("description").Value.ToString(),
                                        LastUpdate = string.IsNullOrEmpty(x.Element("pubDate").Value.ToString()) ? "" :
                                            DateTime.Parse(x.Element("pubDate").Value.ToString())
                                            .ToString("f", CultureInfo.CreateSpecificCulture("en-US")),
                                        Author = x.Element("author").Value.ToString()
                                    }).ToList();

                                }
                                break;
                            }
                    }

                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="request">The user info.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public UserAuthenticationResponseDto AuthenticateUser(UserAuthenticationRequestDto request)
        {
            UserAuthenticationResponseDto response = InitiateResponse<UserAuthenticationRequestDto, UserAuthenticationResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Insert)
                {
                    string userEmail = request.UserInfo.UserEmail.AsString();
                    int userKey = request.UserInfo.UserKey.AsInt();
                    string userId = request.UserInfo.UserId.AsString();

                    UserInfoModel user = this._userInfoDataRepository
                        .GetList(x => (x.UserEmail.Equals(userEmail) ||
                            x.UserKey.Equals(userKey) ||
                            x.UserId.Equals(userId)) &&
                            x.StatusId.Equals((int)StatusEnum.Active)).FirstOrDefault();

                    if (user == null)
                    {
                        request.UserInfo.StatusId = (int)StatusEnum.Active;
                        request.UserInfo.CreatedOn = DateTime.Now;

                        this._userInfoDataRepository.Insert(request.UserInfo);
                        if (request.UserInfo.UserKey > 0)
                        {
                            response.UserInfo = request.UserInfo;
                        }
                    }
                    else
                        response.UserInfo = user;

                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Adds the page to fevorite.
        /// </summary>
        /// <param name="request">The page.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoritePageResponseDto AddPageToFevorite(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString() && userResponse.UserInfo != null
                    && request.Action == CrudAction.Insert)
                {
                    int userKey = userResponse.UserInfo.UserKey;
                    if (!userResponse.UserInfo.FavoritePages.Any(x => x.UserKey.Equals(userKey)
                            && x.PageItemId.Equals(request.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active)))
                    {
                        PageItemModel pageItem = this._pageItemDataRepository.Get(request.PageItemId);
                        if (pageItem != null)
                        {
                            FavoritePageModel favoritePage = new FavoritePageModel
                            {
                                PageId = pageItem.PageId,
                                PageItemId = pageItem.PageItemId,
                                UserKey = userResponse.UserInfo.UserKey,
                                StatusId = (int)StatusEnum.Active,
                                CreatedBy = userResponse.UserInfo.UserKey,
                                CreatedOn = DateTime.Now
                            };
                            this._favoritePageDataRepository.Insert(favoritePage);
                            if (favoritePage.FavoritePageId > 0)
                            {
                                favoritePage.PageItem = this._pageItemDataRepository.Get(favoritePage.PageItemId);
                                userResponse.UserInfo.FavoritePages.Add(favoritePage);
                            }
                        }
                    }
                    response.UserInfo = userResponse.UserInfo;
                    response.ResponseStatus = RespStatus.Success.ToString();
                    return response;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Removes the page from fevorite.
        /// </summary>
        /// <param name="request">The page.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoritePageResponseDto RemovePageFromFevorite(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                userResponse.UserInfo.FavoritePages = GetFavoritePagesByUser(userResponse.UserInfo.UserKey);

                if (userResponse.ResponseStatus == RespStatus.Success.ToString() && userResponse.UserInfo != null
                    && request.Action == CrudAction.Delete)
                {
                    int userKey = userResponse.UserInfo.UserKey;
                    Func<FavoritePageModel, bool> predicate = (x) => x.UserKey.Equals(userKey)
                        && x.PageItemId.Equals(request.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active);

                    if (!userResponse.UserInfo.FavoritePages.IsEmptyCollection() &&
                        userResponse.UserInfo.FavoritePages.Any(predicate))
                    {
                        FavoritePageModel favoritePage = userResponse.UserInfo.FavoritePages.FirstOrDefault(predicate);
                        userResponse.UserInfo.FavoritePages.RemoveAll(new Predicate<FavoritePageModel>(predicate));
                        favoritePage.StatusId = (int)StatusEnum.Inactive;
                        this._favoritePageDataRepository.Update(favoritePage);
                    }
                    response.UserInfo = userResponse.UserInfo;
                    response.ResponseStatus = RespStatus.Success.ToString();
                    return response;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the fevorite pages by user.
        /// </summary>
        /// <param name="request">The page.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoritePageResponseDto GetFevoritePagesByUser(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.BulkSelect)
                {
                    userResponse.UserInfo.FavoritePages = GetFavoritePagesByUser(userResponse.UserInfo.UserKey);
                    response.UserInfo = userResponse.UserInfo;
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Adds the user image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public HttpResponseMessage AddUserImage(UserImageRequestDto request)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            UserImageResponseDto response = InitiateResponse<UserImageRequestDto, UserImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Insert)
                {
                    if (System.Web.HttpContext.Current.Request.Files != null
                        && System.Web.HttpContext.Current.Request.Files.Count > 0
                        && userResponse.UserInfo != null)
                    {
                        string imagePath = UploadImage(userResponse.UserInfo.UserKey, System.Web.HttpContext.Current.Request.Files.ToPostedFileBase());
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            UserImageModel userImage = new UserImageModel
                            {
                                UserImageUrl = imagePath,
                                UserKey = userResponse.UserInfo.UserKey,
                                StatusId = (int)StatusEnum.Active,
                                CreatedBy = userResponse.UserInfo.UserKey,
                                CreatedOn = DateTime.Now
                            };
                            this._userImageDataRepository.Insert(userImage);
                            if (userImage.UserImageId > 0)
                            {
                                userResponse.UserInfo.UserImages.Add(userImage);
                                response.UserInfo = userResponse.UserInfo;
                                response.ResponseStatus = RespStatus.Success.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            responseMessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return responseMessage;
        }

        /// <summary>
        /// Deletes the user image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoriteImageResponseDto DeleteUserImage(FavoriteImageRequestDto request)
        {
            FavoriteImageResponseDto response = InitiateResponse<FavoriteImageRequestDto, FavoriteImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForFevImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Delete)
                {
                    if (request.UserImageId > 0 && userResponse.UserInfo != null)
                    {
                        UserImageModel userImage = this._userImageDataRepository.Get(request.UserImageId);
                        if (userImage != null)
                        {
                            userImage.StatusId = (int)StatusEnum.Inactive;
                            this._userImageDataRepository.Update(userImage);
                            string filePath = HttpContext.Current.Server.MapPath(userImage.UserImageUrl);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                            userResponse.UserInfo.UserImages.RemoveAll(x => x.UserImageId.Equals(request.UserImageId));
                            response.UserInfo = userResponse.UserInfo;
                            response.ResponseStatus = RespStatus.Success.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Adds the image to fevorite.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoriteImageResponseDto AddImageToFevorite(FavoriteImageRequestDto request)
        {
            FavoriteImageResponseDto response = InitiateResponse<FavoriteImageRequestDto, FavoriteImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForFevImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Insert)
                {
                    if (request.UserImageId > 0 && userResponse.UserInfo != null
                        && userResponse.UserInfo.UserImages.Any(x => x.UserImageId.Equals(request.UserImageId)))
                    {
                        var data = this._favoriteImageDataRepository.GetList
                            (x => x.UserImageId == request.UserImageId).FirstOrDefault();

                        if (data == null)
                        {
                            FavoriteImageModel favoriteImage = new FavoriteImageModel()
                            {
                                UserImageId = request.UserImageId,
                                StatusId = (int)StatusEnum.Active,
                                CreatedBy = userResponse.UserInfo.UserKey,
                                CreatedOn = DateTime.Now
                            };

                            this._favoriteImageDataRepository.Insert(favoriteImage);
                            if (favoriteImage.FavoriteImageId > 0)
                                MarkFevoriteOnOff(userResponse.UserInfo, userResponse.UserInfo.UserKey, request.UserImageId, true);
                        }
                        else if (data.StatusId == (int)StatusEnum.Inactive)
                        {
                            data.StatusId = (int)StatusEnum.Active;
                            this._favoriteImageDataRepository.Update(data);
                            MarkFevoriteOnOff(userResponse.UserInfo, userResponse.UserInfo.UserKey, request.UserImageId, true);
                        }

                        response.UserInfo = userResponse.UserInfo;
                        response.ResponseStatus = RespStatus.Success.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Removes the image from fevorite.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public FavoriteImageResponseDto RemoveImageFromFevorite(FavoriteImageRequestDto request)
        {
            FavoriteImageResponseDto response = InitiateResponse<FavoriteImageRequestDto, FavoriteImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForFevImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Delete)
                {
                    if (request.UserImageId > 0 && userResponse.UserInfo != null
                        && userResponse.UserInfo.UserImages.Any(x => x.UserImageId.Equals(request.UserImageId)))
                    {
                        var data = this._favoriteImageDataRepository.GetList
                            (x => x.UserImageId == request.UserImageId).FirstOrDefault();

                        if (data != null && data.StatusId == (int)StatusEnum.Active)
                        {
                            data.StatusId = (int)StatusEnum.Inactive;
                            this._favoriteImageDataRepository.Update(data);
                            MarkFevoriteOnOff(userResponse.UserInfo, userResponse.UserInfo.UserKey, request.UserImageId, false);
                        }
                        response.UserInfo = userResponse.UserInfo;
                        response.ResponseStatus = RespStatus.Success.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the page banner.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public PageBannerResponseDto GetPageBanner(PageBannerRequestDto request)
        {
            PageBannerResponseDto response = InitiateResponse<PageBannerRequestDto, PageBannerResponseDto>(request);
            try
            {
                BannerModel banner = null;
                if (request.Action == CrudAction.Select)
                {
                    if (request.PageId > 0 || request.PageItemId > 0)
                    {
                        var page = this._pageBannerDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageItemId == request.PageItemId).FirstOrDefault();

                        if (page != null)
                        {
                            banner = !page.Banner.IsSplashImage ? page.Banner : new BannerModel();
                        }
                    }
                    if (banner == null)
                    {
                        banner = this._bannerDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.IsDefault == true && x.IsSplashImage == false).FirstOrDefault();
                        response.Banner = banner;
                    }
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the splash image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public SplashImageResponseDto GetSplashImage(SplashImageRequestDto request)
        {
            SplashImageResponseDto response = InitiateResponse<SplashImageRequestDto, SplashImageResponseDto>(request);
            try
            {
                Func<string, string, string> getAttribute = (imagePath, key) =>
                {
                    Image im = Image.FromFile(imagePath);
                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    var allPoprItem = im.PropertyItems;
                    const int metTitle = 0x10e;
                    var title = allPoprItem.FirstOrDefault(x => x.Id == metTitle);
                    return encoding.GetString(title.Value).Replace("\0", "");
                };

                if (request.Action == CrudAction.Select)
                {
                    var splashImages = this._splashImageDataRepository.GetList
                        (x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                    if (!splashImages.IsEmptyCollection())
                    {
                        random = new Random();
                        SplashImageModel splashImage = splashImages.ElementAt(random.Next(0, splashImages.Count()));
                        if (splashImage != null)
                        {
                            response.SplashImageId = splashImage.SplashImageId;
                            response.ImageUrl = splashImage.ImageUrl;
                            response.RedirectionUrl = splashImage.SponsorUrl;
                            response.ResponseStatus = RespStatus.Success.ToString();
                        }
                    }

                    //string fileLocation = CommonUtility.GetAppSetting<string>("SplashImagePath");
                    //string splashImage = this.getRandomFile(HttpContext.Current.Server.MapPath(fileLocation));
                    //response.ImageUrl = Path.GetFileName(splashImage);
                    //response.RedirectionUrl = getAttribute(splashImage, "Title");
                    //response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost()]
        public EventResponseDto GetEvents(EventRequestDto request)
        {
            EventResponseDto response = InitiateResponse<EventRequestDto, EventResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Select)
                {
                    response.RedirectionUrl = CommonUtility.GetAppSetting<string>("EventUrl");
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Hits the sponsor.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HitCounterResponseDto HitSponsor(HitCounterRequestDto request)
        {
            HitCounterResponseDto response = InitiateResponse<HitCounterRequestDto, HitCounterResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Insert)
                {
                    HitCounterModel hitInfo = request.HitInfo;
                    hitInfo.StatusId = (int)StatusEnum.Active;
                    hitInfo.HitOn = DateTime.Now;
                    hitInfo.ClientIdentiy = GetIPAddress();

                    this._hitCounterDataRepository.Insert(hitInfo);
                    if (hitInfo.HitCounterId > 0)
                    {
                        response.ResponseStatus = RespStatus.Success.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Marks the read notification.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public NotificationReadResponseDto MarkReadNotification(NotificationReadRequestDto request)
        {
            NotificationReadResponseDto response = InitiateResponse<NotificationReadRequestDto, NotificationReadResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Update)
                {
                    NotificationLogModel notificationLog = new NotificationLogModel
                    {
                        DeviceId = request.DeviceId,
                        NotifiactionToken = request.NotifiactionToken
                    };
                    var result = this._notificationLogDataRepository.MarkReadNotificationLog(notificationLog);
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        #region Private Members
        /// <summary>
        /// Authentications the user for page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private UserAuthenticationResponseDto AuthUserForPage(FavoritePageRequestDto request)
        {
            UserAuthenticationResponseDto response = new UserAuthenticationResponseDto(request.RequestToken);
            try
            {
                UserAuthenticationRequestDto userRequest = new UserAuthenticationRequestDto()
                {
                    RequestToken = request.RequestToken,
                    AuthToken = request.AuthToken,
                    UserInfo = request.UserInfo,
                    Action = CrudAction.Insert
                };

                response = this.AuthenticateUser(userRequest);
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Authentications the user for image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private UserAuthenticationResponseDto AuthUserForImage(UserImageRequestDto request)
        {
            UserAuthenticationResponseDto response = new UserAuthenticationResponseDto(request.RequestToken);
            try
            {
                UserAuthenticationRequestDto userRequest = new UserAuthenticationRequestDto()
                {
                    RequestToken = request.RequestToken,
                    AuthToken = request.AuthToken,
                    UserInfo = request.UserInfo,
                    Action = CrudAction.Insert
                };

                response = this.AuthenticateUser(userRequest);
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Authentications the user for fav image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private UserAuthenticationResponseDto AuthUserForFevImage(FavoriteImageRequestDto request)
        {
            UserAuthenticationResponseDto response = new UserAuthenticationResponseDto(request.RequestToken);
            try
            {
                UserAuthenticationRequestDto userRequest = new UserAuthenticationRequestDto()
                {
                    RequestToken = request.RequestToken,
                    AuthToken = request.AuthToken,
                    UserInfo = request.UserInfo,
                    Action = CrudAction.Insert
                };

                response = this.AuthenticateUser(userRequest);
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
                ex.ExceptionValueTracker(request);
            }
            return response;
        }

        /// <summary>
        /// Gets the favorite pages by user.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns></returns>
        private List<FavoritePageModel> GetFavoritePagesByUser(int userKey)
        {
            try
            {
                List<FavoritePageModel> favoritePages = this._favoritePageDataRepository
                    .GetList(x => x.UserKey.Equals(userKey)
                    && x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                favoritePages.ForEach(x =>
                {
                    if (x.PageItem == null)
                        x.PageItem = this._pageItemDataRepository.Get(x.PageItemId);
                });

                if (!favoritePages.IsEmptyCollection())
                    return favoritePages;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(userKey);
            }
            return new List<FavoritePageModel>();
        }

        /// <summary>
        /// Gets the images by user.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns></returns>
        private List<UserImageModel> GetImagesByUser(int userKey)
        {
            try
            {
                List<UserImageModel> userImages = this._userImageDataRepository
                    .GetList(x => x.UserKey.Equals(userKey)
                    && x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                return userImages;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(userKey);
            }
            return new List<UserImageModel>();
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns></returns>
        private string UploadImage(int userKey, List<HttpPostedFileBase> files)
        {
            string finalImagePath = string.Empty;
            try
            {
                HttpPostedFileBase uploadedFile = files.FirstOrDefault();
                uploadedFile.InputStream.Seek(0, SeekOrigin.Begin);
                using (Image image = Image.FromStream(uploadedFile.InputStream))
                {
                    string virtualPath = string.Format("~/Upload/Capture/{0}", userKey);
                    string filePath = HostingEnvironment.MapPath(virtualPath);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    string fileName = string.Format("{0:N}_{1}.jpg", Guid.NewGuid(), DateTime.Now.Ticks);
                    string fullPath = Path.Combine(filePath, fileName);

                    string watermarkText = CommonUtility.GetAppSetting<string>("WatermarkText");
                    string watermarkImage = CommonUtility.GetAppSetting<string>("WatermarkImage");

                    string imagePath = CommonUtility.SetWatermarkTextWithImage
                        (image, watermarkText, HostingEnvironment.MapPath(string.Format("~/Styles/Images/{0}", watermarkImage)),
                        filePath, 80, fileName, fullPath);

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        finalImagePath = string.Format("{0}/{1}", virtualPath, imagePath);
                    }
                }

                return finalImagePath;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(userKey);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the random file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string getRandomFile(string path)
        {
            string file = null;
            if (!string.IsNullOrEmpty(path))
            {
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                try
                {
                    var di = new DirectoryInfo(path);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    random = new Random();
                    file = rgFiles.ElementAt(random.Next(0, rgFiles.Count())).FullName;
                }
                // probably should only catch specific exceptions
                // throwable by the above methods.
                catch { }
            }
            return file;
        }

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <returns></returns>
        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// Marks the fevorite on off.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <param name="userKey">The user key.</param>
        /// <param name="userImageId">The user image identifier.</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void MarkFevoriteOnOff(UserInfoModel userInfo, int userKey, int userImageId, bool status)
        {
            userInfo.UserImages.ForEach(x =>
            {
                if (x.UserImageId == userImageId && x.UserKey == userKey)
                {
                    x.IsFavorite = status;
                    return;
                }
            });
        }

        /// <summary>
        /// Initiates the response.
        /// </summary>
        /// <typeparam name="TIn">The type of the in.</typeparam>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private TOut InitiateResponse<TIn, TOut>(TIn request)
            where TIn : RequestBase
            where TOut : ResponseBase, new()
        {
            TOut response = (TOut)Activator.CreateInstance(typeof(TOut), new object[] { request.RequestToken });
            response.ResponseStatus = RespStatus.Failure.ToString();
            return response;
        }
        #endregion
    }
}
