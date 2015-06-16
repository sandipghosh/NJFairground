
namespace NJFairground.Web.Filters
{
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System;
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlHelpers
    {
        private const string description = "description";
        private const string keywords = "keywords";
        private const string author = "author";
        private const string copyright = "copyright";
        private const string applicationName = "application-name";

        private const string ogTitle = "og:title";
        private const string ogType = "og:type";
        private const string ogImage = "og:image";
        private const string ogUrl = "og:url";
        private const string ogDescription = "og:description";

        private const string twitterCard = "twitter:card";
        private const string twitterTitle = "twitter:title";
        private const string twitterDescription = "twitter:description";
        private const string twitterImage = "twitter:image";

        public static MvcHtmlString Meta(this HtmlHelper html, MetaInfoViewModel metaInfo)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                Func<string, string, string> metaFormatter = (name, content) =>
                {
                    return (string.IsNullOrEmpty(content)) ? string.Empty :
                        string.Format("<meta name=\"{0}\" content=\"{1}\" />", name, content);
                };

                // for Google
                sb.AppendLine(metaFormatter(description, metaInfo.Description));
                sb.AppendLine(metaFormatter(keywords, metaInfo.Keyword));
                sb.AppendLine(metaFormatter(author, metaInfo.Author));
                sb.AppendLine(metaFormatter(copyright, metaInfo.Copyright));
                sb.AppendLine(metaFormatter(applicationName, metaInfo.Application));

                //for Facebook
                sb.AppendLine(metaFormatter(ogTitle, metaInfo.Title));
                sb.AppendLine(metaFormatter(ogType, "article"));
                sb.AppendLine(metaFormatter(ogImage, metaInfo.Image));
                sb.AppendLine(metaFormatter(ogUrl, metaInfo.Url));
                sb.AppendLine(metaFormatter(ogDescription, metaInfo.Description));

                //for Twitter
                sb.AppendLine(metaFormatter(twitterCard, "summary"));
                sb.AppendLine(metaFormatter(twitterTitle, metaInfo.Title));
                sb.AppendLine(metaFormatter(twitterDescription, metaInfo.Description));
                sb.AppendLine(metaFormatter(twitterImage, metaInfo.Image));
            }
            catch (Exception ex)
            {
                ex.ExceptionValueTracker(html, metaInfo);
            }

            return new MvcHtmlString(sb.ToString());
        }
    }
}