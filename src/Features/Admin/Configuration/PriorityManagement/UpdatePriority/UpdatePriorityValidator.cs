using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.UpdatePriority
{
    public class UpdatePriorityValidator : AbstractValidator<UpdatePriorityRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdatePriorityValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.PriorityId)
                .NotEmpty().WithMessage(localizer["Priority ID is required."])
                .MustAsync(async (id, ct) => await _context.Priorities.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["Priority not found."]);

            RuleFor(x => x.PriorityName)
                .NotEmpty().WithMessage(localizer["PriorityName is required."])
                .MaximumLength(150).WithMessage(localizer["PriorityName cannot exceed 150 characters."]);
        }
    }
}