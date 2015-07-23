
namespace NJFairground.Web.Areas.Admin.Models
{
    using NJFairground.Web.Utilities;
    public class UserCredentialModel
    {
        public UserCredentialModel()
        {
            this.RedirectUrl = "~/Admin/Home/Index".ToBase64Encode();
        }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string RedirectUrl { get; set; }
    }
}