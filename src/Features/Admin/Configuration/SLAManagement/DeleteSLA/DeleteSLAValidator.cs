using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.DeleteSLA
{
    public class DeleteSLAValidator : AbstractValidator<DeleteSLARequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteSLAValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.SLAId)
                .NotEmpty().WithMessage(localizer["SLA ID is required."])
                .MustAsync(async (id, ct) => await _context.SLAs.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["SLA not found."]);
        }
    }
}