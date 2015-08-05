
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using NJFairground.Web.Models;

    public class FacebookFeedReader : FeedReader, IFeedReader
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
                            response = jsonFeed["data"].Select(x => new RssFeedModel
                                {
                                    Title = GetStringFromHtmlWithoutSpc(x["message"].AsString(), 30),
                                    TitleUrl = x["link"].AsString(),
                                    ImageLink = x["link"].AsString(),
                                    ImageUrl = GetImageLinkFromAttachment(x),
                                    Content = GetStringFromHtmlWithoutSpc(x["message"].AsString()),
                                    LastUpdate = (x["updated_time"] ?? x["created_time"]).AsString(),
                                    Author = (x["from"] != null) ? x["from"]["name"].AsString() : ""
                                }).AsParallel().ToList();
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
        /// Gets the image link from attachment.
        /// </summary>
        /// <param name="jToken">The j token.</param>
        /// <returns></returns>
        public string GetImageLinkFromAttachment(JToken jToken) 
        {
            try
            {
                string highResImageUrl = jToken.SelectToken("$.attachments.data[0].media.image.src").AsString();
                return string.IsNullOrEmpty(highResImageUrl) ? jToken["picture"].AsString() : highResImageUrl;
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the URL with access token.
        /// </summary>
        /// <returns></returns>
        private string GetUrlWithAccessToken()
        {
            try
            {
                string selectedFields = "fields=id,from,name,caption,description,message,picture,link,created_time,updated_time,attachments";
                string accessToken = ReadUrl(FacebookAuthUrl);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    return string.Format(FacebookFeedUrl, selectedFields, accessToken);
                }
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker();
            }
            return string.Empty;
        }
    }
}