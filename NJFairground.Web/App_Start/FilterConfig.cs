

namespace NJFairground.Web
{
    using System.Web;
    using System.Web.Mvc;
    using NJFairground.Web.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            OutputCacheAttribute cashAttr = new OutputCacheAttribute { 
                VaryByParam = "*",
                Duration = 0,
                NoStore = true
            };
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SaveMeFilterAttribute());
            filters.Add(cashAttr);
        }
    }
}