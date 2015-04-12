using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJFairground.Web.Models
{
    public class RssFeedModel
    {
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string LastUpdate { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}