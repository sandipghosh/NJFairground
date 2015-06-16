using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJFairground.Web.Models
{
    using NJFairground.Web.Utilities;

    public class MetaInfoViewModel
    {
        public string Title { get; set; }

        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.description = CommonUtility.ScrubHtml(value); }
        }

        private string image;
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Author { get { return CommonUtility.GetAppSetting<string>("FairName"); } }
        public string Application { get { return "Sussex Country Fairground"; } }
        public string Copyright { get { return CommonUtility.GetAppSetting<string>("WatermarkText"); } }
    }
}