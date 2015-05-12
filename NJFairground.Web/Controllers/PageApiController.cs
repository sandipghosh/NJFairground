

namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.DTO.RequestDto;
    using NJFairground.Web.DTO.ResponseDto;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;
    using System.Xml.Linq;

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
            IPageBannerDataRepository pageBannerDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
            this._userInfoDataRepository = userInfoDataRepository;
            this._favoritePageDataRepository = favoritePageDataRepository;
            this._favoriteImageDataRepository = favoriteImageDataRepository;
            this._userImageDataRepository = userImageDataRepository;
            this._bannerDataRepository = bannerDataRepository;
            this._pageBannerDataRepository = pageBannerDataRepository;
        }

        /// <summary>
        /// Gets the application information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
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
        [HttpPost()]
        public PageResponseDto GetPage(PageRequestDto request)
        {
            PageResponseDto response = InitiateResponse<PageRequestDto, PageResponseDto>(request);
            try
            {
                if (request.PageId > 0)
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
        [HttpPost()]
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
        [HttpPost()]
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
        [HttpPost()]
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
                    var rssFeedAsString = CommonUtility.GetRSSFeedasString(feedLink);
                    switch (request.FeedRequestFor)
                    {
                        case FeedFor.Facebook:
                        case FeedFor.Twitter:
                        case FeedFor.Pinterest:
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

                                break;
                            }
                        case FeedFor.Instagram:
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
        [HttpPost()]
        public UserAuthenticationResponseDto AuthenticateUser(UserAuthenticationRequestDto request)
        {
            UserAuthenticationResponseDto response = InitiateResponse<UserAuthenticationRequestDto, UserAuthenticationResponseDto>(request);
            try
            {
                if (request.Action == CrudAction.Insert)
                {
                    UserInfoModel user = this._userInfoDataRepository
                        .GetList(x => x.UserEmail.Equals(request.UserInfo.UserEmail)
                            || x.UserKey.Equals(request.UserInfo.UserKey)).FirstOrDefault();

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
        [HttpPost()]
        public FavoritePageResponseDto AddPageToFevorite(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString() && userResponse.UserInfo != null
                    && request.Action == CrudAction.Insert)
                {
                    if (!userResponse.UserInfo.FavoritePages.IsEmptyCollection() &&
                        !userResponse.UserInfo.FavoritePages.Any(x => x.UserKey.Equals(userResponse.UserInfo.UserKey)
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
        [HttpPost()]
        public FavoritePageResponseDto RemovePageFromFevorite(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString() && userResponse.UserInfo != null
                    && request.Action == CrudAction.Delete)
                {
                    Func<FavoritePageModel, bool> predicate = (x) => x.UserKey.Equals(userResponse.UserInfo.UserKey)
                        && x.PageItemId.Equals(request.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active);

                    if (!userResponse.UserInfo.FavoritePages.IsEmptyCollection() &&
                        !userResponse.UserInfo.FavoritePages.Any(predicate))
                    {
                        FavoritePageModel favoritePage = userResponse.UserInfo.FavoritePages.FirstOrDefault(predicate);
                        favoritePage.StatusId = (int)StatusEnum.Inactive;
                        this._favoritePageDataRepository.Update(favoritePage);
                        userResponse.UserInfo.FavoritePages.RemoveAll(new Predicate<FavoritePageModel>(predicate));
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
        [HttpPost()]
        public FavoritePageResponseDto GetFevoritePagesByUser(FavoritePageRequestDto request)
        {
            FavoritePageResponseDto response = InitiateResponse<FavoritePageRequestDto, FavoritePageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForPage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.BulkSelect)
                {
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
        [HttpPost()]
        public UserImageResponseDto AddUserImage(UserImageRequestDto request)
        {
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
                                return response;
                            }
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
        /// Deletes the user image.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
        public UserImageResponseDto DeleteUserImage(UserImageRequestDto request)
        {
            UserImageResponseDto response = InitiateResponse<UserImageRequestDto, UserImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForImage(request);
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
                            if (File.Exists(userImage.ImageUrl))
                            {
                                File.Delete(userImage.ImageUrl);
                            }
                            userResponse.UserInfo.UserImages.RemoveAll(x => x.UserImageId.Equals(request.UserImageId));
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
        [HttpPost()]
        public UserImageResponseDto AddImageToFevorite(UserImageRequestDto request)
        {
            UserImageResponseDto response = InitiateResponse<UserImageRequestDto, UserImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Insert)
                {
                    if (request.UserImageId > 0 && userResponse.UserInfo != null)
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
                            {
                                userResponse.UserInfo.UserImages.ForEach(x =>
                                {
                                    if (x.UserImageId == request.UserImageId && x.UserKey == userResponse.UserInfo.UserKey)
                                    {
                                        x.IsFavorite = true;
                                        return;
                                    }
                                });
                            }
                        }
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
        [HttpPost()]
        public UserImageResponseDto RemoveImageFromFevorite(UserImageRequestDto request)
        {
            UserImageResponseDto response = InitiateResponse<UserImageRequestDto, UserImageResponseDto>(request);
            try
            {
                var userResponse = this.AuthUserForImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Delete)
                {
                    if (request.UserImageId > 0 && userResponse.UserInfo != null)
                    {
                        var data = this._favoriteImageDataRepository.GetList
                            (x => x.UserImageId == request.UserImageId).FirstOrDefault();

                        if (data != null)
                        {
                            data.StatusId = (int)StatusEnum.Inactive;
                            this._favoriteImageDataRepository.Update(data);

                            userResponse.UserInfo.UserImages.ForEach(x =>
                            {
                                if (x.UserImageId == request.UserImageId && x.UserKey == userResponse.UserInfo.UserKey)
                                {
                                    x.IsFavorite = false;
                                    return;
                                }
                            });
                        }
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
        [HttpPost()]
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
                            banner = page.Banner;
                        }
                    }
                    if (banner == null)
                    {
                        banner = this._bannerDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.IsDefault == true).FirstOrDefault();
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
        /// Gets the favorite pages by user.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns></returns>
        private List<PageItemModel> GetFavoritePagesByUser(int userKey)
        {
            try
            {
                List<FavoritePageModel> favoritePages = this._favoritePageDataRepository
                    .GetList(x => x.UserKey.Equals(userKey)
                    && x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                return favoritePages.Select(x => x.PageItem).ToList();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(userKey);
            }
            return new List<PageItemModel>();
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
    }
}
