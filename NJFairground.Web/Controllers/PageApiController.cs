

namespace NJFairground.Web.Controllers
{
    using System.Web.Http;
    using System;
    using System.Linq;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Data.Interface;
    using NJFairground.Web.Data.Implementation;
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.DTO.RequestDto;
    using NJFairground.Web.DTO.ResponseDto;
    using System.Collections.Generic;
    using System.Globalization;
    using System.ServiceModel.Syndication;
    using System.Xml.Linq;

    public class PageApiController : ApiController
    {
        private readonly IPageDataRepository _pageDataRepository;
        private readonly IPageItemDataRepository _pageItemDataRepository;

        public PageApiController(IPageDataRepository pageDataRepository,
            IPageItemDataRepository pageItemDataRepository)
        {
            this._pageDataRepository = pageDataRepository;
            this._pageItemDataRepository = pageItemDataRepository;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
        public PageResponseDto GetPage(PageRequestDto request)
        {
            PageResponseDto response = new PageResponseDto();
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
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
                throw;
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
            PageItemResponseDto response = new PageItemResponseDto();
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
            response.CorrelationToken = request.RequestToken;
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
            return new MapResponseDto();
        }

        /// <summary>
        /// Gets the media feed.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost()]
        public RssFeedResponseDto GetMediaFeed(RssFeedRequestDto request)
        {
            RssFeedResponseDto response = new RssFeedResponseDto();
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
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(request);
            }
            return response;
        }
    }
}
