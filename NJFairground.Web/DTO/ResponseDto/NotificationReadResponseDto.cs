
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;

    public class NotificationReadResponseDto : ResponseBase
    {
        public NotificationReadResponseDto()
        {
        }

        public NotificationReadResponseDto(string requestToken)
            : base(requestToken)
        {
        }
    }
}