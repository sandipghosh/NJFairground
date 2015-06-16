
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;

    public class HitCounterResponseDto : ResponseBase
    {
        public HitCounterResponseDto()
        {
        }

        public HitCounterResponseDto(string requestToken)
            : base(requestToken)
        {
        }
    }
}