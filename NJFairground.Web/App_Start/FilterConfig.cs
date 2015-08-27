

namespace NJFairground.Web
{
    using System.Web.Http.Filters;
    using System.Web.Mvc;
    using NJFairground.Web.Filters;

    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            OutputCacheAttribute cashAttr = new OutputCacheAttribute { 
                VaryByParam = "*",
                Duration = 0,
                NoStore = true
            };
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SaveMeMVCFilterAttribute());
            filters.Add(cashAttr);
        }

        /// <summary>
        /// Registers the global API filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new SaveMeApiFilterAttribute());
        }
    }
}