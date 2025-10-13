using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.UpdateStatus
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusRequest, UpdateStatusResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateStatusHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateStatusResponse> Handle(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.Statuses.FirstAsync(p => p.Id == request.StatusId, cancellationToken);

            // update fields
            item.StatusName = request.StatusName?.Trim();
            item.SortOrder = request.SortOrder;
            item.ColorCode = request.ColorCode?.Trim();
            item.IsFinalStatus = request.IsFinalStatus;
            item.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateStatusResponse
            {
                Message = _localizer["Status updated successfully."]
            };
        }
    }
}
