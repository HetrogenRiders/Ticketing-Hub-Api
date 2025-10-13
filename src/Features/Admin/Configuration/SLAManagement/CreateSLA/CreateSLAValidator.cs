using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.CreateSLA
{
    public class CreateSLAValidator : AbstractValidator<CreateSLARequest>
    {
        public CreateSLAValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.SLAName)
                .NotEmpty().WithMessage(localizer["SLAName is required."])
                .MaximumLength(150).WithMessage(localizer["SLAName cannot exceed 150 characters."]);
        }
    }
}