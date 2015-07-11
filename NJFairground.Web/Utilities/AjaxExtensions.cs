

namespace NJFairground.Web.Utilities
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    public static class AjaxExtensions
    {
        public static IHtmlString MyActionLink(
        this AjaxHelper ajaxHelper,
        string linkText,
        string actionName,
        string controllerName,
        RouteValueDictionary routeValues,
        AjaxOptions ajaxOptions)
        {
            var targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
            return MvcHtmlString.Create(ajaxHelper.GenerateLink(linkText, targetUrl, ajaxOptions ?? new AjaxOptions(), null));
        }

        private static string GenerateLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string targetUrl,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes
        )
        {
            var a = new TagBuilder("a")
            {
                InnerHtml = linkText
            };
            a.MergeAttributes<string, object>(htmlAttributes);
            a.MergeAttribute("href", targetUrl);
            a.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());
            return a.ToString(TagRenderMode.Normal);
        }
    }
}