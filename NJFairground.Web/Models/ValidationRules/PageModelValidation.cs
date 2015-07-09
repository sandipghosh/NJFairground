
namespace NJFairground.Web.Models.ValidationRules
{
    using FluentValidation;

    public class PageModelValidation : AbstractValidator<PageModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModelValidation"/> class.
        /// </summary>
        public PageModelValidation()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.PageName).NotNull().WithMessage("Name should not be empty");
            RuleFor(x => x.PageDesc).NotNull().WithMessage("Description should not be empty"); 
        }
    }
}