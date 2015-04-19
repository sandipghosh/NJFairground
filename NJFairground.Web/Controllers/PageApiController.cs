﻿

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
    using System.Net.Http;
    using System.ServiceModel.Syndication;
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

        public PageApiController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository,
            IUserInfoDataRepository userInfoDataRepository,
            IFavoritePageDataRepository favoritePageDataRepository,
            IFavoriteImageDataRepository favoriteImageDataRepository,
            IUserImageDataRepository userImageDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
            this._userInfoDataRepository = userInfoDataRepository;
            this._favoritePageDataRepository = favoritePageDataRepository;
            this._favoriteImageDataRepository = favoriteImageDataRepository;
            this._userImageDataRepository = userImageDataRepository;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
        public PageResponseDto GetPage(PageRequestDto request)
        {
            PageResponseDto response = new PageResponseDto(request.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
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
                                .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                        else
                            response.Pages = this._pageDataRepository
                                .GetList(request.ItemIndex, request.ItemCount, x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();

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
            PageItemResponseDto response = new PageItemResponseDto(request.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
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
                            x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageId.Equals(request.PageId)).ToList();
                    else
                        response.PageItems = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageId.Equals(request.PageId)).ToList();
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
                else if (request.Action == CrudAction.BulkSelect)
                {
                    if (!string.IsNullOrEmpty(request.Filter))
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.PageItems = this._pageItemDataRepository
                                .GetList(request.ItemIndex, request.ItemCount, request.GetExpression<PageItemModel>()).ToList();
                        else
                            response.PageItems = this._pageItemDataRepository
                                .GetList(request.GetExpression<PageItemModel>()).ToList();
                    }
                    else
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.PageItems = this._pageItemDataRepository
                                .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                        else
                            response.PageItems = this._pageItemDataRepository
                                .GetList(request.ItemIndex, request.ItemCount, x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
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
            RssFeedResponseDto response = new RssFeedResponseDto(request.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
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
            UserAuthenticationResponseDto response = new UserAuthenticationResponseDto(request.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
            try
            {
                if (request.Action == CrudAction.Insert)
                {
                    UserInfoModel user = this._userInfoDataRepository
                                .GetList(x => x.UserEmail.Equals(request.UserInfo.UserEmail)).FirstOrDefault();

                    if (user != null)
                    {
                        this._userInfoDataRepository.Insert(request.UserInfo);
                        if (request.UserInfo.UserKey > 0)
                        {
                            response.UserInfo = user;
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
        /// Adds the page to fevorite.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpPost()]
        public FavoritePageResponseDto AddPageToFevorite(FavoritePageRequestDto page)
        {
            FavoritePageResponseDto response = new FavoritePageResponseDto(page.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
            try
            {
                var userResponse = this.AuthUserForPage(page);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && page.Action == CrudAction.Insert)
                {
                    FavoritePageModel favoritePage = this._favoritePageDataRepository
                        .GetList(x => x.UserKey.Equals(userResponse.UserInfo.UserKey)
                        && x.PageItemId.Equals(page.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active)).FirstOrDefault();

                    if (favoritePage == null)
                    {
                        PageItemModel pageItem = this._pageItemDataRepository.Get(page.PageItemId);
                        if (pageItem != null)
                        {
                            favoritePage = new FavoritePageModel
                            {
                                PageId = pageItem.PageId,
                                PageItemId = favoritePage.PageItemId,
                                UserKey = favoritePage.UserKey,
                                StatusId = (int)StatusEnum.Active,
                                CreatedBy = favoritePage.UserKey,
                                CreatedOn = DateTime.Now
                            };
                            this._favoritePageDataRepository.Insert(favoritePage);
                            if (favoritePage.FavoritePageId > 0)
                            {
                                response.UserInfo = userResponse.UserInfo;
                                response.FavoritePages = GetFavoritePagesByUser(userResponse.UserInfo.UserKey);
                                response.ResponseStatus = RespStatus.Success.ToString();
                                return response;
                            }
                        }
                    }
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(page);
            }
            return response;
        }

        /// <summary>
        /// Removes the page from fevorite.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpPost()]
        public FavoritePageResponseDto RemovePageFromFevorite(FavoritePageRequestDto page)
        {
            FavoritePageResponseDto response = new FavoritePageResponseDto(page.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
            try
            {
                var userResponse = this.AuthUserForPage(page);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && page.Action == CrudAction.Delete)
                {
                    FavoritePageModel favoritePage = this._favoritePageDataRepository
                        .GetList(x => x.UserKey.Equals(userResponse.UserInfo.UserKey)
                        && x.PageItemId.Equals(page.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active)).FirstOrDefault();

                    if (favoritePage != null)
                    {
                        favoritePage.StatusId = (int)StatusEnum.Inactive;
                        this._favoritePageDataRepository.Update(favoritePage);

                        response.UserInfo = userResponse.UserInfo;
                        response.FavoritePages = GetFavoritePagesByUser(userResponse.UserInfo.UserKey);
                        response.ResponseStatus = RespStatus.Success.ToString();
                        return response;
                    }
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(page);
            }
            return response;
        }

        /// <summary>
        /// Gets the fevorite pages by user.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        [HttpPost()]
        public FavoritePageResponseDto GetFevoritePagesByUser(FavoritePageRequestDto page)
        {
            FavoritePageResponseDto response = new FavoritePageResponseDto(page.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
            try
            {
                var userResponse = this.AuthUserForPage(page);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && page.Action == CrudAction.BulkSelect)
                {
                    response.UserInfo = userResponse.UserInfo;
                    response.FavoritePages = GetFavoritePagesByUser(userResponse.UserInfo.UserKey);
                    response.ResponseStatus = RespStatus.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(page);
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
            UserImageResponseDto response = new UserImageResponseDto(request.RequestToken)
            {
                ResponseStatus = RespStatus.Failure.ToString()
            };
            try
            {
                var userResponse = this.AuthUserForImage(request);
                if (userResponse.ResponseStatus == RespStatus.Success.ToString()
                    && request.Action == CrudAction.Insert)
                {
                    if (Request.Content.IsMimeMultipartContent())
                    {
                        string imagePath = UploadImage(userResponse.UserInfo.UserKey);
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
                            response.UserInfo = userResponse.UserInfo;
                            response.UserImages = GetImagesByUser(userResponse.UserInfo.UserKey);
                            response.ResponseStatus = RespStatus.Success.ToString();
                            return response;
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

                userImages.ForEach(x => x.UserImageUrl = CommonUtility.ResolveServerUrl(x.UserImageUrl, false));
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
        private string UploadImage(int userKey)
        {
            string finalImagePath = string.Empty;
            try
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider())
                    .ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        Image image = Image.FromStream(stream);
                        //var testName = content.Headers.ContentDisposition.Name;
                        string virtualPath = string.Format("~/Upload/Capture/{0}", userKey);
                        string filePath = HostingEnvironment.MapPath(virtualPath);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        string fileName = string.Format("{0:N}_{1}.jpg", Guid.NewGuid(), DateTime.Now.Ticks);
                        string fullPath = Path.Combine(filePath, fileName);

                        string imagePath = CommonUtility.SetWatermark(image, "@Fairground", filePath, 30, fileName, fullPath);
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            finalImagePath = Path.Combine(virtualPath, imagePath);
                        }
                        //String[] headerValues = (String[])Request.Headers.GetValues("UniqueId");
                        //String fileName = headerValues[0] + ".jpg";
                        //String fullPath = Path.Combine(filePath, fileName);
                        //image.Save(fullPath);
                       // return string.Empty;
                    }
                });

                return finalImagePath;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(userKey);
            }
            return string.Empty;
        }
    }
}
