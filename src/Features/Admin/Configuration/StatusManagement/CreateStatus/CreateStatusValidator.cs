using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.CreateStatus
{
    public class CreateStatusValidator : AbstractValidator<CreateStatusRequest>
    {
        public CreateStatusValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.StatusName)
                .NotEmpty().WithMessage(localizer["StatusName is required."])
                .MaximumLength(150).WithMessage(localizer["StatusName cannot exceed 150 characters."]);
        }
    }
}