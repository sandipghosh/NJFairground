
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;
    using System;

    public class SplashImageModel : BaseModel
    {
        public int SplashImageId { get; set; }
        public string SplashName { get; set; }
        public string ImageUrl { get; set; }
        public string SponsorUrl { get; set; }
        public int Position { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}