
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml.Linq;
    using NJFairground.Web.Models;

    public class PinterestFeedReader : FeedReader, IFeedReader
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
                string feedData = ReadUrl(PinterestFeedUrl);
                if (!string.IsNullOrEmpty(feedData))
                {
                    Func<string, string, string> setImageLink = (titleLink, imageLink)
                        => titleLink.Contains(imageLink) ? titleLink : imageLink;

                    SyndicationFeed feed = SyndicationFeed.Load(XDocument.Parse(feedData).CreateReader());
                    response = feed.Items.Select(x => new RssFeedModel
                    {
                        Title = x.Title.Text.AsString(),
                        TitleUrl = (x.Links.FirstOrDefault() == null) ? string.Empty : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                        ImageLink = GetImageLinkFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        ImageUrl = GetImageUrlFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        Content = GetStringFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                            x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                            x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                        Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.AsString()
                    }).ToList();

                    foreach (var item in response)
                    {
                        item.ImageLink = setImageLink(item.TitleUrl, item.ImageLink);
                    }
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