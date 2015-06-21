using System;

namespace NJFairground.Web.Models
{
    public class HitCounterDetailViewModel
    {
        public string SponsorType { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SponsorUrl { get; set; }
        public string ClientIdentity { get; set; }
        public DateTime HitOn { get; set; }
    }
}