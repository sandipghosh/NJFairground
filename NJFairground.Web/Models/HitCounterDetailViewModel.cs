

namespace NJFairground.Web.Models
{
    using System;
    using NJFairground.Web.Models.Base;

    public class HitCounterDetailViewModel : BaseModel
    {
        public string SponsorType { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SponsorUrl { get; set; }
        public string ClientIdentity { get; set; }
        public DateTime HitOn { get; set; }
    }
}