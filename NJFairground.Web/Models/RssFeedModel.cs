
namespace NJFairground.Web.Models
{
    public class RssFeedModel
    {
        public RssFeedModel()
        {
            this.Title = "";
            this.TitleUrl = "";
            this.LastUpdate = "";
            this.Author = "";
            this.ImageLink = "";
            this.ImageUrl = "";
            this.Content = "";
            this.TextContent = "";
        }

        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string LastUpdate { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public string TextContent { get; set; }
    }
}