using FluentValidation;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.UpdateTicket
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketRequest>
    {
        public UpdateTicketValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.TicketId)
                .NotEmpty().WithMessage(localizer["Ticket ID is required."]);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(localizer["Title is required."])
                .MaximumLength(200).WithMessage(localizer["Title cannot exceed 200 characters."]);

            RuleFor(x => x.PriorityId)
                .NotEmpty().WithMessage(localizer["Priority is required."]);

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage(localizer["Status is required."]);
        }
    }
}
