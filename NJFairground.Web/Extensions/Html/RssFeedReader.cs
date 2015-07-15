

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

    //internal class RssFeedEntity
    //{
    //    public string Title { get; set; }
    //    public string TitleUrl { get; set; }
    //    public string LastUpdate { get; set; }
    //    public string Author { get; set; }
    //    public string ImageLink { get; set; }
    //    public string ImageUrl { get; set; }
    //    public string Content { get; set; }
    //}

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
                //string rssFeedAsString = string.Empty;
                FeedFor feedfor = FeedFor.Facebook;
                switch(feedLink)
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

                NJFairground.Web.Helper.IFeedReader feedReader = NJFairground.Web.Helper
                    .SocialFeedReader.GetSocialMediaFeed(feedfor);

                feedItems = feedReader.Read().ToList();

                //if (feedLink == "Facebook:RssFeed") {
                //    rssFeedAsString = CommonUtility.GetFacebookJsonFeedAsString();

                //    if (!string.IsNullOrEmpty(rssFeedAsString))
                //    {
                //        JObject jsonFeed = JObject.Parse(rssFeedAsString);
                //        feedItems = jsonFeed["data"].Select(x => new RssFeedModel
                //        {
                //            Title = (x["message"].AsString().Length > 30) ?
                //                x["message"].AsString().Substring(0, 30) + ".." : x["message"].AsString(),
                //            TitleUrl = x["link"].AsString(),
                //            ImageLink = x["link"].AsString(),
                //            ImageUrl = x["picture"].AsString(),
                //            Content = x["message"].AsString(),
                //            LastUpdate = (x["updated_time"] ?? x["created_time"]).AsString(),
                //            Author = (x["from"] != null) ? x["from"]["name"].AsString() : ""
                //        }).ToList();
                //    }
                //}
                //else
                //{
                //    rssFeedAsString = CommonUtility.GetRSSFeedAsString(feedLink);
                //    if (!string.IsNullOrEmpty(rssFeedAsString))
                //    {
                //        // convert feed to XML using LINQ to XML and finally create new XmlReader object
                //        var feed = SyndicationFeed.Load(XDocument.Parse(rssFeedAsString).CreateReader());
                //        feedItems = feed.Items.Select(x => new RssFeedModel
                //        {
                //            Title = x.Title.Text,
                //            TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                //            Content = ((TextSyndicationContent)(x.Content ?? x.Summary)).Text,
                //            LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                //                x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                //                x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                //            Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.ToString()
                //        }).ToList(); 
                //    } 
                //}

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
            NJFairground.Web.Helper.IFeedReader feedReader = NJFairground.Web.Helper
                .SocialFeedReader.GetSocialMediaFeed(FeedFor.Instagram);

            List<RssFeedModel> feedItems = feedReader.Read().ToList();

            //var rssFeedAsString = CommonUtility.GetRSSFeedAsString(feedLink);

            //XDocument doc = XDocument.Parse(rssFeedAsString);
            //List<RssFeedModel> feedItems = doc.Descendants("item").Select(x => new RssFeedModel
            //{
            //    Title = x.Element("title").Value.ToString(),
            //    TitleUrl = x.Element("link").Value.ToString(),
            //    ImageLink = x.Element("image").Element("link").Value.ToString(),
            //    ImageUrl = x.Element("image").Element("link").Value.ToString(),
            //    Content = x.Element("description").Value.ToString(),
            //    LastUpdate = string.IsNullOrEmpty(x.Element("pubDate").Value.ToString()) ? "" :
            //        DateTime.Parse(x.Element("pubDate").Value.ToString())
            //        .ToString("f", CultureInfo.CreateSpecificCulture("en-US")),
            //    Author = x.Element("author").Value.ToString()
            //}).ToList();

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