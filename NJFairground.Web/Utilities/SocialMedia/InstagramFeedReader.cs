

namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using NJFairground.Web.Models;

    public class InstagramFeedReader : FeedReader, IFeedReader
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
                string feedData = ReadUrl(InstagramFeedUrl);
                if (!string.IsNullOrEmpty(feedData))
                {
                    XDocument doc = XDocument.Parse(feedData);
                    response = doc.Descendants("item").Select(x => new RssFeedModel
                    {
                        Title = GetStringFromHtmlWithoutSpc(x.Element("title").Value.AsString(), 30),
                        TitleUrl = x.Element("link").Value.AsString(),
                        ImageLink = x.Element("image").Element("link").Value.AsString(),
                        ImageUrl = x.Element("image").Element("url").Value.AsString(),
                        Content = ExtractContent(GetStringFromHtmlWithoutSpc(x.Element("description").Value.AsString())),
                        LastUpdate = string.IsNullOrEmpty(x.Element("pubDate").Value.AsString()) ? "" :
                            DateTime.Parse(x.Element("pubDate").Value.AsString())
                            .ToString("f", CultureInfo.CreateSpecificCulture("en-US")),
                        Author = x.Element("author").Value.AsString()
                    }).AsParallel().ToList();

                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return response;
        }

        private string ExtractContent(string content)
        {
            try
            {
                return content.Split('#')[0];
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(content);
            }
            return string.Empty;
        }
    }
}