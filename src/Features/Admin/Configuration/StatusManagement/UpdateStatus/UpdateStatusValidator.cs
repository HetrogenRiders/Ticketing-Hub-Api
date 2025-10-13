using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.UpdateStatus
{
    public class UpdateStatusValidator : AbstractValidator<UpdateStatusRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateStatusValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage(localizer["Status ID is required."])
                .MustAsync(async (id, ct) => await _context.Statuses.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["Status not found."]);

            RuleFor(x => x.StatusName)
                .NotEmpty().WithMessage(localizer["StatusName is required."])
                .MaximumLength(150).WithMessage(localizer["StatusName cannot exceed 150 characters."]);
        }
    }
}