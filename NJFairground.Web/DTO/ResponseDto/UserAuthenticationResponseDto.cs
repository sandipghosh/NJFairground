
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;

    public class UserAuthenticationResponseDto : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationResponseDto"/> class.
        /// </summary>
        public UserAuthenticationResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationResponseDto"/> class.
        /// </summary>
        /// <param name="requestToken">The request token.</param>
        public UserAuthenticationResponseDto(string requestToken)
            : base(requestToken)
        {
        }

        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        /// <value>
        /// The user information.
        /// </value>
        public UserInfoModel UserInfo { get; set; }
    }
}