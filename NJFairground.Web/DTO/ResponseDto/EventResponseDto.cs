namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;

    public class EventResponseDto : ResponseBase
    {
        public EventResponseDto()
        {
        }

        public EventResponseDto(string requestToken)
            : base(requestToken)
        {
        }
        public string RedirectionUrl { get; set; }
    }
}