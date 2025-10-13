using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;


namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.DeletePriority
{
    public class DeletePriorityValidator : AbstractValidator<DeletePriorityRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeletePriorityValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.PriorityId)
                .NotEmpty().WithMessage(localizer["Priority ID is required."])
                .MustAsync(async (id, ct) => await _context.Priorities.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["Priority not found."]);
        }
    }
}