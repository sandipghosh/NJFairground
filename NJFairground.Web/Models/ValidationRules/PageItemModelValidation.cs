

namespace NJFairground.Web.Models.ValidationRules
{
    using FluentValidation;

    public class PageItemModelValidation : AbstractValidator<PageItemModel>
    {
        public PageItemModelValidation()
        {
            RuleFor(x => x.PageId).NotNull();
            RuleFor(x => x.PageHeaderText).NotNull().Length(100);
            RuleFor(x => x.PageItemImage).NotNull();
        }
    }
}