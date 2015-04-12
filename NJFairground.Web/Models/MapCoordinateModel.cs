
namespace NJFairground.Web.Models
{
    using NJFairground.Web.Models.Base;

    public class MapCoordinateModel:BaseModel
    {
        public string Title { get; set; }
        public string Coords { get; set; }
        public string Shape { get; set; }
    }
}