

namespace NJFairground.Web.Models.ValidationRules
{
    using FluentValidation;
    using FluentValidation.Mvc;

    public class PageItemModelValidation : AbstractValidator<PageItemModel>
    {
        public PageItemModelValidation()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.PageHeaderText).NotNull().WithMessage("Header should not be empty");
            RuleFor(x => x.PageSubHeaderText).NotNull().WithMessage("Sub-Header should not be empty"); 
            RuleFor(x => x.PageItemDetailText).NotNull().WithMessage("Content should not be empty");
        }
    }
}