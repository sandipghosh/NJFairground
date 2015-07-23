using System.Web.Mvc;

namespace NJFairground.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Logout",
                "Admin/Logout",
                new { controller = "Login", action = "Logout" },
                new[] { "NJFairground.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Login",
                "Admin/Login/{redirectionUrl}",
                new {controller="Login",  action = "Index", redirectionUrl = UrlParameter.Optional },
                new[] { "NJFairground.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "NJFairground.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
