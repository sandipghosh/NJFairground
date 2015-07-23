
namespace NJFairground.Web.Areas.Admin.Models.ValidationRules
{
    using FluentValidation;

    public class UserCredentialModelValidation : AbstractValidator<UserCredentialModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageItemModelValidation"/> class.
        /// </summary>
        public UserCredentialModelValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id should not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
        }
    }
}