using NJFairground.Web.Areas.Admin.Models;
using NJFairground.Web.Utilities;
using System.Web.Mvc;

namespace NJFairground.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string redirectionUrl = null)
        {
            UserCredentialModel user = new UserCredentialModel();
            user.RedirectUrl = string.IsNullOrEmpty(redirectionUrl) ? user.RedirectUrl : redirectionUrl;
            return View(user);
        }

        /// <summary>
        /// Indexes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(UserCredentialModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserId != CommonUtility.GetAppSetting<string>("AdminUserId"))
                {
                    ModelState.AddModelError("UserId", "User Id is invalid");
                    return View(user);
                }
                if (user.Password != CommonUtility.GetAppSetting<string>("AdminPwd"))
                {
                    ModelState.AddModelError("Password", "Password is invalid");
                    return View(user);
                }
                else
                {
                    Session["UserId"] = user.UserId;
                    if (!string.IsNullOrEmpty(user.RedirectUrl))
                    {
                        return Redirect(Url.Content(user.RedirectUrl.ToBase64Decode()));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
            }
            else
            {
                return View(user);
            }
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectPermanent(Url.Content("~/Admin/Login"));
        }
    }
}