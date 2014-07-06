﻿

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
                var rssFeedAsString = GetRSSFeedasString(feedLink);

                // convert feed to XML using LINQ to XML and finally create new XmlReader object
                var feed = SyndicationFeed.Load(XDocument.Parse(rssFeedAsString).CreateReader());

                List<RssFeedEntity> feeds = feed.Items.Select(x => new RssFeedEntity
                {
                    Title = x.Title.Text,
                    TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                    Content = ((TextSyndicationContent)(x.Content ?? x.Summary)).Text,
                    LastUpdate = x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US")),
                    Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.ToString()
                }).ToList();
                return new MvcHtmlString(GetHtmlFromRss(feeds));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static MvcHtmlString ReadInstagramRss(this HtmlHelper htmlHelper, string feedLink)
        {
            var rssFeedAsString = GetRSSFeedasString(feedLink);

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

        private static string GetRSSFeedasString(string feedLink)
        {
            var webClient = new WebClient();
            string feedUrl = CommonUtility
                .GetAppSetting<string>(feedLink).Replace("&amp;", "&");

            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            // fetch feed as string
            var content = webClient.OpenRead(feedUrl);
            var contentReader = new StreamReader(content);
            var rssFeedAsString = contentReader.ReadToEnd();
            return rssFeedAsString;
        }

        private static string GetHtmlFromRss(List<RssFeedEntity> feeds)
        {
            TagBuilder feedItemWrapper = new TagBuilder("div");
            feedItemWrapper.Attributes.Add("class", "feedItemWrapper");

            feeds.ForEach(x =>
            {
                TagBuilder feedItem = new TagBuilder("div");
                feedItem.Attributes.Add("class", "feed-entry");

                TagBuilder titleHeader = new TagBuilder("h3");

                TagBuilder titleLink = new TagBuilder("a");
                titleLink.Attributes.Add("href", x.TitleUrl);
                titleLink.InnerHtml = x.Title;
                titleHeader.InnerHtml += titleLink.ToString();

                TagBuilder lastUpdated = new TagBuilder("div");
                lastUpdated.Attributes.Add("class", "lastUpdated");
                lastUpdated.InnerHtml = x.LastUpdate;
                titleHeader.InnerHtml += lastUpdated.ToString();

                TagBuilder feedAuthor = new TagBuilder("div");
                feedAuthor.Attributes.Add("class", "feedAuthor");
                feedAuthor.InnerHtml = x.Author;
                titleHeader.InnerHtml += feedAuthor.ToString();
                feedItem.InnerHtml += titleHeader.ToString();

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