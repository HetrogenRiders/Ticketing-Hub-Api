using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.CreateSubCategory
{
    public class CreateSubCategoryValidator : AbstractValidator<CreateSubCategoryRequest>
    {
        public CreateSubCategoryValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage(localizer["CategoryId is required."]);
        }
    }
}