﻿

namespace NJFairground.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("FuckYou", "Base/FuckYou/{status}",
                new { controller = "Base", action = "FuckYou" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "LandingPage", action = "Index", id = UrlParameter.Optional },
                new[] { "NJFairground.Web.Controllers" }
            );
        }
    }
}