
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using HtmlAgilityPack;
    using NJFairground.Web.Models;

    public abstract class FeedReader
    {
        public string FacebookAuthUrl { get { return CommonUtility.GetAppSetting<string>("Facebook:AuthTokenUrl"); } }
        public string FacebookFeedUrl { get { return CommonUtility.GetAppSetting<string>("Facebook:JsonFeed"); } }
        public string TwitterFeedUrl { get { return CommonUtility.GetAppSetting<string>("Twitter:RssFeed"); } }
        public string PinterestFeedUrl { get { return CommonUtility.GetAppSetting<string>("Pinterest:RssFeed"); } }
        public string InstagramFeedUrl { get { return CommonUtility.GetAppSetting<string>("Instagram:RssFeed"); } }

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
                @"\n|\r\n|<.*?>|[^\u0000-\u007F]", string.Empty);

            filterData = Regex.Replace(filterData, @"\s\s+", string.Empty);

            if (maxlength > 0)
                filterData = filterData.Length > maxlength ? filterData.Substring(0, maxlength) + ".." : filterData;

            return filterData;
        }

        /// <summary>
        /// Gets the image link from HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        protected string GetImageUrlFromHtml(string html)
        {
            try
            {
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    HtmlNode anchorNode = htmlDocument.DocumentNode.Descendants("img")
                        .Where(x => x.Attributes.Contains("src") && !string.IsNullOrEmpty(x.Attributes["src"].Value))
                        .FirstOrDefaultCustom();

                    return anchorNode.Attributes["src"].Value.AsString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(html);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the URL link from HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        protected string GetImageLinkFromHtml(string html)
        {
            try
            {
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    HtmlNode anchorNode = htmlDocument.DocumentNode.Descendants("a")
                        .Where(x => x.Attributes.Contains("href") && !string.IsNullOrEmpty(x.Attributes["href"].Value)
                            && x.SelectSingleNode("img") != null)
                        .FirstOrDefaultCustom();

                    return anchorNode.Attributes["href"].Value.AsString();
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(html);
            }
            return string.Empty;
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
}