
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System;
    using NJFairground.Web.Models;

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
}