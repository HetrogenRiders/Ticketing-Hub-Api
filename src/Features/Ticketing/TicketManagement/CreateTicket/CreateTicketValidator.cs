using FluentValidation;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(localizer["Title is required."])
                .MaximumLength(200).WithMessage(localizer["Title cannot exceed 200 characters."]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department is required."]);

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage(localizer["Category is required."]);

            RuleFor(x => x.PriorityId)
                .NotEmpty().WithMessage(localizer["Priority is required."]);

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage(localizer["Status is required."]);
        }
    }
}
