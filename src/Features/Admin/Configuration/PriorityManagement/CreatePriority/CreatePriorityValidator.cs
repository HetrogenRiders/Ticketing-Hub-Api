using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.CreatePriority
{
    public class CreatePriorityValidator : AbstractValidator<CreatePriorityRequest>
    {
        public CreatePriorityValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.PriorityName)
                .NotEmpty().WithMessage(localizer["PriorityName is required."])
                .MaximumLength(150).WithMessage(localizer["PriorityName cannot exceed 150 characters."]);
        }
    }
}