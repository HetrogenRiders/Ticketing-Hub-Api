using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.UpdateSLA
{
    public class UpdateSLAHandler : IRequestHandler<UpdateSLARequest, UpdateSLAResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateSLAHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateSLAResponse> Handle(UpdateSLARequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SLAs.FirstAsync(p => p.Id == request.SLAId, cancellationToken);

            // update fields
            item.SLAName = request.SLAName?.Trim();
            item.ResponseTimeInHours = request.ResponseTimeInHours;
            item.ResolutionTimeInHours = request.ResolutionTimeInHours;
            item.EscalationLevel1Hours = request.EscalationLevel1Hours;
            item.EscalationLevel2Hours = request.EscalationLevel2Hours;
            item.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateSLAResponse
            {
                Message = _localizer["SLA updated successfully."]
            };
        }
    }
}
