

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
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel.Syndication;
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
            PageResponseDto response = new PageResponseDto(request.RequestToken);
            try
            {
                if (request.PageId > 0)
                {
                    response.Page = this._pageDataRepository.Get(request.PageId);
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
                    }
                    else
                    {
                        if (request.ItemCount > 0 && request.ItemIndex > 0)
                            response.Pages = this._pageDataRepository
                                .GetList(x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                        else
                            response.Pages = this._pageDataRepository
                                .GetList(request.ItemIndex, request.ItemCount, x => x.StatusId.Equals((int)StatusEnum.Active)).ToList();
                    }
                }
                response.ResponseStatus = RespStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
            PageItemResponseDto response = new PageItemResponseDto(request.RequestToken);
            try
            {
                if (request.PageItemId > 0)
                {
                    response.PageItem = this._pageItemDataRepository.Get(request.PageId);
                }
                if (request.PageId > 0 && request.Action == CrudAction.BulkSelect)
                {
                    if (request.ItemCount > 0 && request.ItemIndex > 0)
                        response.PageItems = this._pageItemDataRepository.GetList(request.ItemIndex, request.ItemCount,
                            x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageId.Equals(request.PageId)).ToList();
                    else
                        response.PageItems = this._pageItemDataRepository.GetList(x => x.StatusId.Equals((int)StatusEnum.Active)
                            && x.PageId.Equals(request.PageId)).ToList();
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
                }

                response.ResponseStatus = RespStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
            RssFeedResponseDto response = new RssFeedResponseDto(request.RequestToken);
            try
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
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
            UserAuthenticationResponseDto response = new UserAuthenticationResponseDto(request.RequestToken);
            try
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
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
        public ResponseBase AddPageToFevorite(FavoritePageRequestDto page)
        {
            ResponseBase response = new ResponseBase(page.RequestToken);
            try
            {
                FavoritePageModel favoritePage = this._favoritePageDataRepository.GetList(x => x.UserKey.Equals(page.UserKey)
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
                            response.ResponseStatus = RespStatus.Success.ToString();
                            return response;
                        }
                    }
                }
                response.ResponseStatus = RespStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
        public ResponseBase RemovePageFromFevorite(FavoritePageRequestDto page)
        {
            ResponseBase response = new ResponseBase(page.RequestToken);
            try
            {
                FavoritePageModel favoritePage = this._favoritePageDataRepository.GetList(x => x.UserKey.Equals(page.UserKey)
                    && x.PageItemId.Equals(page.PageItemId) && x.StatusId.Equals((int)StatusEnum.Active)).FirstOrDefault();

                if (favoritePage != null)
                {
                    favoritePage.StatusId = (int)StatusEnum.Inactive;
                    this._favoritePageDataRepository.Update(favoritePage);

                    response.ResponseStatus = RespStatus.Success.ToString();
                    return response;
                }
                response.ResponseStatus = RespStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
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
            FavoritePageResponseDto response = new FavoritePageResponseDto(page.RequestToken);
            try
            {
                List<FavoritePageModel> favoritePages = this._favoritePageDataRepository.GetList(x => x.UserKey.Equals(page.UserKey)
                    && x.StatusId.Equals((int)StatusEnum.Active)).ToList();

                response.FavoritePages = favoritePages.Select(x=>x.PageItem).ToList();
                response.ResponseStatus = RespStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseStatus = RespStatus.Failure.ToString();
                ex.ExceptionValueTracker(page);
            }
            return response;
        }
    }
}
