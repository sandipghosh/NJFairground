
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;

    public class AppInfoModel : BaseModel
    {
        public string FairYear { get; set; }
        public string FairName { get; set; }
        public string FairSubname { get; set; }
        public string FairCatchline { get; set; }
        public string FairVenue { get; set; }
    }
}