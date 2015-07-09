﻿
namespace NJFairground.Web.Controllers
{
    using NJFairground.Web.Controllers.Base;
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        public ActionResult Index(string msg)
        {
            ViewBag.ErrorMessage = msg;
            return View();
        }
    }
}
