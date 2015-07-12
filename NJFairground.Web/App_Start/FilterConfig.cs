using System.Web;
using System.Web.Mvc;

namespace NJFairground.Web
{
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
            filters.Add(cashAttr);
        }
    }
}