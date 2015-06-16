

namespace NJFairground.Web.DTO.RequestDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class HitCounterRequestDto : RequestBase
    {
        public HitCounterModel HitInfo { get; set; }
    }
}
