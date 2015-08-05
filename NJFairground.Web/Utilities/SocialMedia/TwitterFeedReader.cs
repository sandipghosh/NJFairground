
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml.Linq;
    using NJFairground.Web.Models;

    public class TwitterFeedReader : FeedReader, IFeedReader
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
                string feedData = ReadUrl(TwitterFeedUrl);
                if (!string.IsNullOrEmpty(feedData))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(XDocument.Parse(feedData).CreateReader());
                    response = feed.Items.Select(x => new RssFeedModel
                    {
                        Title = x.Title.Text.AsString(),
                        TitleUrl = (x.Links.FirstOrDefault() == null) ? 
                            GetImageLinkFromHtml(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString())
                            : x.Links.FirstOrDefault().Uri.AbsoluteUri,
                        Content = GetStringFromHtmlWithoutSpc(((TextSyndicationContent)(x.Content ?? x.Summary)).Text.AsString()),
                        LastUpdate = (x.LastUpdatedTime.Year == 1 ?
                            x.PublishDate.ToString("f", CultureInfo.CreateSpecificCulture("en-US")) :
                            x.LastUpdatedTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))),
                        Author = (x.Authors.LastOrDefault() == null) ? string.Empty : x.Authors.LastOrDefault().Name.AsString()
                    }).AsParallel().ToList();
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