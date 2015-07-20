

namespace NJFairground.Web.Extensions.Html
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using NJFairground.Web.Utilities.SocialMedia;

    public static class RssFeedReader
    {
        /// <summary>
        /// Reads the RSS.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="feedLink">The feed link.</param>
        /// <returns></returns>
        public static MvcHtmlString ReadRss(this HtmlHelper htmlHelper, string feedLink)
        {
            try
            {
                List<RssFeedModel> feedItems = new List<RssFeedModel>();
                FeedFor feedfor = FeedFor.Facebook;
                switch (feedLink)
                {
                    case "Facebook:RssFeed":
                        feedfor = FeedFor.Facebook;
                        break;
                    case "Twitter:RssFeed":
                        feedfor = FeedFor.Twitter;
                        break;
                    case "Pinterest:RssFeed":
                        feedfor = FeedFor.Pinterest;
                        break;
                }

                NJFairground.Web.Utilities.SocialMedia.IFeedReader feedReader
                    = NJFairground.Web.Utilities.SocialMedia.SocialFeedReader.GetSocialMediaFeed(feedfor);

                feedItems = feedReader.Read().ToList();
                return new MvcHtmlString(GetHtmlFromRss(feedItems));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(feedLink);
            }
            return new MvcHtmlString(string.Empty);
        }

        /// <summary>
        /// Reads the instagram RSS.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="feedLink">The feed link.</param>
        /// <returns></returns>
        public static MvcHtmlString ReadInstagramRss(this HtmlHelper htmlHelper, string feedLink)
        {
            NJFairground.Web.Utilities.SocialMedia.IFeedReader feedReader
                = NJFairground.Web.Utilities.SocialMedia.SocialFeedReader.GetSocialMediaFeed(FeedFor.Instagram);

            List<RssFeedModel> feedItems = feedReader.Read().ToList();
            return new MvcHtmlString(GetHtmlFromRss(feedItems));
        }

        /// <summary>
        /// Gets the HTML from RSS.
        /// </summary>
        /// <param name="feeds">The feeds.</param>
        /// <returns></returns>
        private static string GetHtmlFromRss(List<RssFeedModel> feeds)
        {
            TagBuilder feedItemWrapper = new TagBuilder("div");
            feedItemWrapper.Attributes.Add("class", "feedItemWrapper");

            feeds.ForEach(x =>
            {
                TagBuilder feedItem = new TagBuilder("div");
                feedItem.Attributes.Add("class", "feed-entry");

                TagBuilder top = new TagBuilder("div");
                top.Attributes.Add("class", "top");

                TagBuilder eventContainer = new TagBuilder("div");
                eventContainer.Attributes.Add("class", "event");
                TagBuilder titleLink = new TagBuilder("a");
                titleLink.Attributes.Add("href", x.TitleUrl);
                titleLink.InnerHtml = x.Title;
                eventContainer.InnerHtml += titleLink.ToString();
                top.InnerHtml += eventContainer.ToString();

                TagBuilder venue = new TagBuilder("div");
                venue.Attributes.Add("class", "venue");

                TagBuilder lastUpdated = new TagBuilder("span");
                lastUpdated.Attributes.Add("class", "lastUpdated");
                lastUpdated.InnerHtml = x.LastUpdate;
                venue.InnerHtml += lastUpdated.ToString();

                TagBuilder feedAuthor = new TagBuilder("span");
                feedAuthor.Attributes.Add("class", "feedAuthor");
                feedAuthor.InnerHtml = x.Author;
                venue.InnerHtml += feedAuthor.ToString();
                top.InnerHtml += venue.ToString();
                feedItem.InnerHtml += top.ToString();

                TagBuilder feedItemContent = new TagBuilder("div");
                feedItemContent.Attributes.Add("class", "feedItemContent");
                feedItemContent.InnerHtml = x.Content;
                feedItem.InnerHtml += feedItemContent.ToString();

                feedItemWrapper.InnerHtml += feedItem.ToString();
            });

            return feedItemWrapper.ToString();
        }
    }
}