
namespace NJFairground.Web.Utilities.SocialMedia
{
    using System.Collections.Generic;
    using NJFairground.Web.Models;

    public interface IFeedReader
    {
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        IList<RssFeedModel> Read();
    }
}
