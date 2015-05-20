

namespace NJFairground.Web.Utilities
{
    using System;
    using System.IO;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Linq;
    using System.Linq;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    internal class RssFeedEntity
    {
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string LastUpdate { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }

    public static class RssFeedReader
    {
        public static MvcHtmlString ReadRss(this HtmlHelper htmlHelper, string feedLink)
        {
            try
            {
                var rssFeedAsString = CommonUtility.GetRSSFeedasString(feedLink);

                // convert feed to XML using LINQ to XML and finally create new XmlReader object
                var feed = SyndicationFeed.Load(XDocument.Parse(rssFeedAsString).CreateReader());

                List<RssFeedEntity> feeds = feed.Items.Select(x => new RssFeedEntity
                {
                    Title = x.Title.Text,
                    TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                    Content = ((TextSyndicationContent)(x.Content ?? x.Summary)).Text,
                    LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                        x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                        x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                    Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.ToString()
                }).ToList();
                return new MvcHtmlString(GetHtmlFromRss(feeds));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(feedLink);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString ReadInstagramRss(this HtmlHelper htmlHelper, string feedLink)
        {
            var rssFeedAsString = CommonUtility.GetRSSFeedasString(feedLink);

            XDocument doc = XDocument.Parse(rssFeedAsString);
            List<RssFeedEntity> feedItems = doc.Descendants("item").Select(x => new RssFeedEntity
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

            return new MvcHtmlString(GetHtmlFromRss(feedItems));
        }

        

        private static string GetHtmlFromRss(List<RssFeedEntity> feeds)
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