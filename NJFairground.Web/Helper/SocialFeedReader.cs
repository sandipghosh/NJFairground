

namespace NJFairground.Web.Helper
{
    using Newtonsoft.Json.Linq;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using System.Xml.Linq;

    public class SocialFeedReader
    {
        /// <summary>
        /// Gets the social media feed.
        /// </summary>
        /// <param name="feedFor">The feed for.</param>
        /// <returns></returns>
        public static IFeedReader GetSocialMediaFeed(FeedFor feedFor)
        {
            IFeedReader feedReader = null;
            try
            {
                switch (feedFor)
                {
                    case FeedFor.Facebook:
                        feedReader = new FacebookFeedReader();
                        break;
                    case FeedFor.Twitter:
                        feedReader = new TwitterFeedReader();
                        break;
                    case FeedFor.Pinterest:
                        feedReader = new PinterestFeedReader();
                        break;
                    case FeedFor.Instagram:
                        feedReader = new InstagramFeedReader();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(feedFor);
            }
            return feedReader;
        }
    }

    public interface IFeedReader
    {
        IList<RssFeedModel> Read();
    }

    public abstract class FeedReader
    {
        /// <summary>
        /// Reads the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected string ReadUrl(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                Func<string, string> replaceAmp = (input) => input.Replace("&amp;", "&");

                if (!string.IsNullOrEmpty(url))
                {
                    webClient = new WebClient();
                    string facebookjson = webClient.DownloadString(replaceAmp(url));
                    return facebookjson;
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(url);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the string from HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        protected string GetStringFromHtml(string html)
        {
            return Regex.Replace(System.Web.HttpUtility.HtmlDecode(html),
                @"[^\u0000-\u007F]", string.Empty);
        }

        /// <summary>
        /// Gets the string from HTML without SPC.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="maxlength">The maxlength.</param>
        /// <returns></returns>
        protected string GetStringFromHtmlWithoutSpc(string html, int maxlength = 0)
        {
            string filterData = Regex.Replace(System.Web.HttpUtility.HtmlDecode(html),
                @"<.*?>|[^\u0000-\u007F]", string.Empty);

            if (maxlength > 0)
                filterData = filterData.Length > maxlength ? filterData.Substring(0, maxlength) + ".." : filterData;

            return filterData;
        }

        /// <summary>
        /// Generates the content HTML.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <returns></returns>
        protected RssFeedModel GenerateContentHtml(RssFeedModel feed)
        {
            try
            {
                TagBuilder pImg = null;
                if (!string.IsNullOrEmpty(feed.ImageUrl))
                {
                    pImg = new TagBuilder("p");
                    TagBuilder a = new TagBuilder("a");
                    a.Attributes.Add("href", string.IsNullOrEmpty(feed.ImageLink) ? "#" : feed.ImageLink);

                    TagBuilder img = new TagBuilder("img");
                    img.Attributes.Add("src", string.IsNullOrEmpty(feed.ImageUrl) ? "#" : feed.ImageUrl);
                    img.Attributes.Add("width", "100%");
                    a.InnerHtml = img.ToString();
                    pImg.InnerHtml += a.ToString();
                }

                TagBuilder pContent = new TagBuilder("p");
                pContent.InnerHtml += GetStringFromHtmlWithoutSpc(feed.Content);
                feed.Content = (pImg == null ? "" : pImg.ToString()) + pContent.ToString();
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(feed);
            }
            return feed;
        }
    }

    public class FacebookFeedReader :FeedReader, IFeedReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IList<RssFeedModel> Read()
        {
            IList<RssFeedModel> response = new List<RssFeedModel>();
            try
            {
                string feedUrl = GetUrlWithAccessToken();
                if (!string.IsNullOrEmpty(feedUrl))
                {
                    string feedData = ReadUrl(feedUrl);
                    if (!string.IsNullOrEmpty(feedData))
                    {
                        JObject jsonFeed = JObject.Parse(feedData);
                        if (jsonFeed != null)
                        {
                            response = jsonFeed["data"].Select(x => GenerateContentHtml
                                (new RssFeedModel
                                {
                                    Title = GetStringFromHtmlWithoutSpc(x["message"].AsString(), 30),
                                    TitleUrl = x["link"].AsString(),
                                    ImageLink = x["link"].AsString(),
                                    ImageUrl = x["picture"].AsString(),
                                    Content = x["message"].AsString(),
                                    LastUpdate = (x["updated_time"] ?? x["created_time"]).AsString(),
                                    Author = (x["from"] != null) ? x["from"]["name"].AsString() : ""
                                })).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return response;
        }

        /// <summary>
        /// Gets the URL with access token.
        /// </summary>
        /// <returns></returns>
        private string GetUrlWithAccessToken()
        {
            try
            {
                string selectedFields = "fields=id,from,name,caption,description,message,picture,link,created_time,updated_time";
                string authTokenUrl = CommonUtility.GetAppSetting<string>("Facebook:AuthTokenUrl");
                string jsonFeedUrl = CommonUtility.GetAppSetting<string>("Facebook:JsonFeed");

                string accessToken = ReadUrl(authTokenUrl);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    return string.Format(jsonFeedUrl, selectedFields, accessToken);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return string.Empty;
        }
    }

    public class TwitterFeedReader :FeedReader, IFeedReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IList<RssFeedModel> Read()
        {
            IList<RssFeedModel> response = new List<RssFeedModel>();
            try
            {
                string feedLink = CommonUtility.GetAppSetting<string>("Twitter:RssFeed");
                string feedData = ReadUrl(feedLink);
                if (!string.IsNullOrEmpty(feedData))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(XDocument.Parse(feedData).CreateReader());
                    response = feed.Items.Select(x => new RssFeedModel
                    {
                        Title = x.Title.Text.AsString(),
                        TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                        Content = GetStringFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                            x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                            x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                        Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.AsString()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return response;
        }
    }

    public class PinterestFeedReader :FeedReader, IFeedReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IList<RssFeedModel> Read()
        {
            IList<RssFeedModel> response = new List<RssFeedModel>();
            try
            {
                string feedLink = CommonUtility.GetAppSetting<string>("Pinterest:RssFeed");
                string feedData = ReadUrl(feedLink);
                if (!string.IsNullOrEmpty(feedData))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(XDocument.Parse(feedData).CreateReader());
                    response = feed.Items.Select(x => new RssFeedModel
                    {
                        Title = x.Title.Text.AsString(),
                        TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                        Content = GetStringFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                            x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                            x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                        Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.AsString()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return response;
        }
    }

    public class InstagramFeedReader :FeedReader, IFeedReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public IList<RssFeedModel> Read()
        {
            IList<RssFeedModel> response = new List<RssFeedModel>();
            try
            {
                string feedLink = CommonUtility.GetAppSetting<string>("Instagram:RssFeed");
                string feedData = ReadUrl(feedLink);
                if (!string.IsNullOrEmpty(feedData))
                {
                    XDocument doc = XDocument.Parse(feedData);
                    response = doc.Descendants("item").Select(x => GenerateContentHtml(new RssFeedModel
                    {
                        Title = GetStringFromHtmlWithoutSpc(x.Element("title").Value.AsString(), 30),
                        TitleUrl = x.Element("link").Value.AsString(),
                        ImageLink = x.Element("image").Element("link").Value.AsString(),
                        ImageUrl = x.Element("image").Element("url").Value.AsString(),
                        Content = x.Element("description").Value.AsString(),
                        LastUpdate = string.IsNullOrEmpty(x.Element("pubDate").Value.AsString()) ? "" :
                            DateTime.Parse(x.Element("pubDate").Value.AsString())
                            .ToString("f", CultureInfo.CreateSpecificCulture("en-US")),
                        Author = x.Element("author").Value.AsString()
                    })).ToList();

                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return response;
        }
    }
}