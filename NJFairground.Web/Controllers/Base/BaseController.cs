
namespace NJFairground.Web.Controllers.Base
{
    using System.Web.Mvc;
    using System.Web.Routing;
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}
