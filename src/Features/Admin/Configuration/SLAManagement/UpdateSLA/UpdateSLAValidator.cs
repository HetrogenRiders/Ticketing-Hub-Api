using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.UpdateSLA
{
    public class UpdateSLAValidator : AbstractValidator<UpdateSLARequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateSLAValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.SLAId)
                .NotEmpty().WithMessage(localizer["SLA ID is required."])
                .MustAsync(async (id, ct) => await _context.SLAs.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["SLA not found."]);

            RuleFor(x => x.SLAName)
                .NotEmpty().WithMessage(localizer["SLAName is required."])
                .MaximumLength(150).WithMessage(localizer["SLAName cannot exceed 150 characters."]);
        }
    }
}